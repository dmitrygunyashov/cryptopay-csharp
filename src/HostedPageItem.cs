using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CryptopayApi
{
    class HostedPageItem
    {
        [JsonProperty("name")]
        public string Name { set; get; }
        [JsonProperty("price")]
        public Decimal Price { set; get; }
        [JsonProperty("quantity")]
        public Decimal Quantity { set; get; }
        [JsonProperty("description")]
        public string Description { set; get; }
        [JsonProperty("vat_rate")]
        public Decimal VatRate { set; get; }
    }
}
