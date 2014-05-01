using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CryptopayApi
{
    class HostedPageInvoice
    {
        [JsonProperty("token")]
        public string Token { set; get; }
        [JsonProperty("success_redirect_url")]
        public string SuccessRedirectUrl { set; get; }
        [JsonProperty("callback_url")]
        public string CallbackUrl { set; get; }
        [JsonProperty("form")]
        public List<string> Form { set; get; }
        [JsonProperty("collect_email")]
        public bool CollectEmail { set; get; }
        [JsonProperty("collect_name")]
        public bool CollectName { set; get; }
        [JsonProperty("collect_address")]
        public bool CollectAddress { set; get; }
        [JsonProperty("collect_phone")]
        public bool CollectPhone { set; get; }
        [JsonProperty("change_quantity")]
        public bool ChangeQuantity { set; get; }
        [JsonProperty("id")]
        public string Id { set; get; }
        [JsonProperty("price")]
        public Decimal Price { set; get; }
        [JsonProperty("currency")]
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
        [JsonProperty("url")]
        public string Url { set; get; }
        [JsonProperty("items")]
        public List<HostedPageItem> Items { set; get; }
    }
}
