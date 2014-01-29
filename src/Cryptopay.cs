using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace CryptopayApi
{
    public class Cryptopay
    {
        private string apiKey { get; set; }
        private const string CRYPTOPAY_URL = "https://cryptopay.me/api/v1/invoices/";
        public Cryptopay(string apiKey) 
        {
            this.apiKey = apiKey;
        }

        public Invoice RequestInvoice(
            int price,
            Currency currency,
            string description,
            string id,
            Dictionary<string, string> callbackParams,
            string successRedirectUrl,
            string errorRedirectUrl,
            string callbackUrl
        ) 
        {
            Invoice invoice = null;
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["api_key"] = apiKey;
                data["price"] = price.ToString();
                data["currency"] = Enum.GetName(typeof(Currency), currency); ;
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
                if (errorRedirectUrl != null)
                    data["error_redirect_url"] = errorRedirectUrl;
                if (callbackUrl != null)
                    data["callback_url"] = callbackUrl;
                var response = wb.UploadValues(CRYPTOPAY_URL, "POST", data);
                var responseString = Encoding.Default.GetString(response);
                System.Diagnostics.Debug.WriteLine(responseString);
                invoice = JsonConvert.DeserializeObject<Invoice>(responseString);
            }
            return invoice;
        }
    }
}
