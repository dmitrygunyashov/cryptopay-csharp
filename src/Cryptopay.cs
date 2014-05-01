using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;

namespace CryptopayApi
{
    class Cryptopay
    {
        private string apiKey { get; set; }
        private static readonly string CRYPTOPAY_BASE_URL = "https://cryptopay.me/api/v1/";
        private static readonly string CRYPTOPAY_BALANCE_URL = CRYPTOPAY_BASE_URL + "balance/";
        private static readonly string CRYPTOPAY_INVOICE_URL = CRYPTOPAY_BASE_URL + "invoices/";
        private static readonly string CRYPTOPAY_BUTTON_URL = CRYPTOPAY_BASE_URL + "buttons/";
        private static readonly string CRYPTOPAY_HOSTED_PAGE_URL = CRYPTOPAY_BASE_URL + "hosted/";

        public Cryptopay(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public Balance RequestBalance()
        {
            Balance balance = null;
            using (var wb = new WebClient())
            {
                wb.Headers.Add("Content-Type", "application/json");
                wb.QueryString.Add("api_key", apiKey);
                var responseString = wb.DownloadString(CRYPTOPAY_BALANCE_URL);
                Dictionary<Currency, Decimal> balanceInCurrencies = JsonConvert.DeserializeObject<Dictionary<Currency, Decimal>>(responseString);
                balance = new Balance(balanceInCurrencies);
            }
            return balance;
        }

        public Invoice RequestInvoice(
            Decimal price,
            Currency currency = Currency.EUR,
            string description = null,
            string id = null,
            string callbackUrl = null,
            string successRedirectUrl = null,
            int confirmationCount = -1,
            Dictionary<string, string> callbackParams = null
        )
        {
            Invoice invoice = null;
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["api_key"] = apiKey;
                data["price"] = price.ToString().Replace(',', '.');
                data["currency"] = currency.ToString();
                if (description != null)
                    data["description"] = description;
                if (id != null)
                    data["id"] = id;
                if (callbackParams != null)
                {
                    foreach (string key in callbackParams.Keys)
                    {
                        string callbackParamName = "callback_params[" + key.Trim() + "]";
                        data[callbackParamName] = callbackParams[key];
                    }
                }
                if (successRedirectUrl != null)
                    data["success_redirect_url"] = successRedirectUrl;
                if (confirmationCount != -1)
                    data["confirmations_count"] = confirmationCount.ToString();
                if (callbackUrl != null)
                    data["callback_url"] = callbackUrl;
                var response = wb.UploadValues(CRYPTOPAY_INVOICE_URL, "POST", data);
                var responseString = Encoding.Default.GetString(response);
                invoice = JsonConvert.DeserializeObject<Invoice>(responseString);
            }
            return invoice;
        }

        public Invoice RequoteInvoice(
            string uuid
        )
        {
            Invoice invoice = null;
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["api_key"] = apiKey;
                var response = wb.UploadValues(CRYPTOPAY_INVOICE_URL + uuid, "PUT", data);
                var responseString = Encoding.Default.GetString(response);
                invoice = JsonConvert.DeserializeObject<Invoice>(responseString);
            }
            return invoice;
        }

        public Invoice RequestInvoiceInfo(
            string uuid
        )
        {
            Invoice invoice = null;
            using (var wb = new WebClient())
            {
                wb.Headers.Add("Content-Type", "application/json");
                wb.QueryString.Add("api_key", apiKey);
                var responseString = wb.DownloadString(CRYPTOPAY_INVOICE_URL + uuid);
                invoice = JsonConvert.DeserializeObject<Invoice>(responseString);
            }
            return invoice;
        }

        public InvoiceCollection ListInvoices(
            int page = -1,
            int perPage = -1,
            DateTime? from = null,
            DateTime? to = null
        )
        {
            InvoiceCollection invoiceCollection = null;
            using (var wb = new WebClient())
            {
                wb.Headers.Add("Content-Type", "application/json");
                wb.QueryString.Add("api_key", apiKey);
                if (page != -1)
                {
                    wb.QueryString.Add("page", page.ToString());
                }
                if (perPage != -1)
                {
                    wb.QueryString.Add("per_page", perPage.ToString());
                }
                if (from != null)
                {
                    wb.QueryString.Add("from", ToUnixTime((DateTime)from).ToString());
                }
                if (to != null)
                {
                    wb.QueryString.Add("to", ToUnixTime((DateTime)to).ToString());
                }
                var responseString = wb.DownloadString(CRYPTOPAY_INVOICE_URL);
                invoiceCollection = JsonConvert.DeserializeObject<InvoiceCollection>(responseString);
            }
            return invoiceCollection;
        }

        public Button CreateButton(
            Decimal price,
            Currency currency = Currency.EUR,
            string name = null
        )
        {
            Button button = null;
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["api_key"] = apiKey;
                data["price"] = price.ToString().Replace(',', '.');
                data["currency"] = currency.ToString();
                if (name != null)
                    data["name"] = name;

                var response = wb.UploadValues(CRYPTOPAY_BUTTON_URL, "POST", data);
                var responseString = Encoding.Default.GetString(response);
                button = JsonConvert.DeserializeObject<Button>(responseString);
            }
            return button;
        }

        public HostedPageInvoice CreateHostedPage(
            List<HostedPageItem> items,
            Currency currency = Currency.EUR,
            string id = null,
            bool collectEmail = false,
            bool collectPhone = false,
            bool collectName = false,
            bool collectAddress = false,
            List<string> form = null,
            bool changeQuantity = true,
            string callbackUrl = null,
            string successRedirectUrl = null
        )
        {
            HostedPageInvoice hostedPageInvoice = null;
            using (var wb = new WebClient())
            {
                wb.Headers.Add("Content-Type", "application/json");
                var data = new HostedPageRequest();
                data.Items = items;
                data.Id = id;
                data.Currency = currency.ToString();
                data.CollectEmail = collectEmail;
                data.CollectPhone = collectPhone;
                data.CollectName = collectName;
                data.CollectAddress = collectAddress;
                data.ChangeQuantity = changeQuantity;
                data.CallbackUrl = callbackUrl;
                data.SuccessRedirectUrl = successRedirectUrl;
                data.Form = form;

                var response = wb.UploadData(CRYPTOPAY_HOSTED_PAGE_URL + "?api_key=" + apiKey, "POST", System.Text.Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data)));
                var responseString = Encoding.Default.GetString(response);

                hostedPageInvoice = JsonConvert.DeserializeObject<HostedPageInvoice>(responseString);
            }
            return hostedPageInvoice;
        }

        public string ValidationHash(
            string uuid,
            Decimal price,
            Currency currency
        ) 
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            string data = String.Format("{0}_{1}_{2}{3}", apiKey, uuid, ((long)price * 100).ToString(), currency.ToString());
            byte[] hashBytes = sha.ComputeHash(System.Text.Encoding.ASCII.GetBytes(data));
            return string.Concat(hashBytes.Select(b => b.ToString("X2")).ToArray()).ToLower();
        } 

        private static long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
    }
}
