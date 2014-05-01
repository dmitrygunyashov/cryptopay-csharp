using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace CryptopayApi
{
    class Invoice
    {
        public string Uuid { set; get; }
        public string Description { set; get; }
        public string Status { set; get; }
        [JsonProperty("btc_price")]
        public Decimal BtcPrice { set; get; }
        [JsonProperty("btc_address")]
        public string BtcAddress { set; get; }
        [JsonProperty("short_id")]
        public string ShortId { set; get; }
        [JsonProperty("callback_params")]
        public Dictionary<string, string> CallbackParams { set; get; }
        [JsonProperty("confirmation_count")]
        public int ConfirmationCount { set; get; }
        [JsonProperty("id")]
        public string Id { set; get; }
        public Decimal Price { set; get; }
        public Currency Currency { set; get; }
        [JsonProperty("created_at")]
        public long CreatedAtTimestamp { set; get; }
        [JsonIgnore]
        public DateTime CreatedAt
        {
            get
            {
                DateTime createdAtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                createdAtDateTime = createdAtDateTime.AddSeconds(this.CreatedAtTimestamp).ToLocalTime();
                return createdAtDateTime;
            }
        }
        [JsonProperty("valid_till")]
        public long ValidTillTimestamp { set; get; }
        [JsonIgnore]
        public DateTime ValidTill
        {
            get
            {
                DateTime validTillDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                validTillDateTime = validTillDateTime.AddSeconds(this.ValidTillTimestamp).ToLocalTime();
                return validTillDateTime;
            }
        }
        [JsonProperty("bitcoin_uri")]
        public string BitcoinUri { set; get; }
        [JsonProperty("callback_url")]
        public string CallbackUrl { set; get; }
        [JsonProperty("success_redirect_url")]
        public string SuccessRedirectUrl { set; get; }
    }
}
