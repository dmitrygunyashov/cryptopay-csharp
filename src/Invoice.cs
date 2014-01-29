using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace CryptopayApi
{
    public class Invoice
    {
        public string Uuid { set; get; }
        public string Description { set; get; }
        public string Status { set; get; }
        [JsonProperty("btc_price")]
        public string BtcPrice { set; get; }
        [JsonProperty("btc_address")]
        public string BtcAddress { set; get; }
        [JsonProperty("short_id")]
        public string ShortId { set; get; }
        [JsonProperty("callback_params")]
        public Dictionary<string, string> CallbackParams { set; get; }
        [JsonProperty("id")]
        public string Id { set; get; }
        public string Price { set; get; }
        public string Currency { set; get; }
        [JsonProperty("created_at")]
        public string CreatedAt { set; get; }
        [JsonProperty("valid_till")]
        public string ValidTill { set; get; }
    }
}
