using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CryptopayApi
{
    class Button
    {
        [JsonProperty("token")]
        public string Token { set; get; }
        [JsonProperty("name")]
        public string Name { set; get; }
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
        [JsonProperty("callback_url")]
        public string CallbackUrl { set; get; }
    }
}
