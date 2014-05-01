using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CryptopayApi
{
    class InvoiceCollection
    {
        [JsonProperty("total")]
        public int Total { set; get; }
        [JsonProperty("page")]
        public int Page { set; get; }
        [JsonProperty("total_pages")]
        public int TotalPages { set; get; }
        public List<Invoice> Invoices { set; get; }
    }
}
