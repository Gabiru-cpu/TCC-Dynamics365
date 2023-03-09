using Microsoft.Xrm.Sdk.Workflow;
using Newtonsoft.Json;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.VO
{
    public class AccountAddressVO
    {
        [JsonProperty("cep")]
        public string cep { get; set; }

        [JsonProperty("logradouro")]
        public string logradouro { get; set; }

        [JsonProperty("complemento")]
        public string complemento { get; set; }

        [JsonProperty("bairro")]
        public string bairro { get; set; }

        [JsonProperty("localidade")]
        public string localidade { get; set; }

        [JsonProperty("uf")]
        public string uf { get; set; }

        [JsonProperty("ibge")]        
        public string ibge { get; set; }

        [JsonProperty("ddd")]
        public string ddd { get; set; }
    }
}
