using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CryptopayApi
{
    class HostedPageRequest
    {
        [JsonProperty("items")]
        public List<HostedPageItem> Items { set; get; }
        [JsonProperty("currency")]
        public string Currency { set; get; }
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { set; get; }
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
        [JsonProperty(PropertyName = "form", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Form { set; get; }
        [JsonProperty(PropertyName = "callback_url", NullValueHandling = NullValueHandling.Ignore)]
        public string CallbackUrl { set; get; }
        [JsonProperty(PropertyName = "success_redirect_url", NullValueHandling = NullValueHandling.Ignore)]
        public string SuccessRedirectUrl { set; get; }
    }
}
