using DynacoopTCC.Logistics.Plugin.VO;
using DynacoopTCC.Logistics.Plugin.DynacoopTCC.LogisticsISV;
using Microsoft.Xrm.Sdk.Workflow;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.Actions
{
    public class BuscaApiViaCEP : ActionCore
    {
        [Input("cep")]
        public InArgument<string> Cep { get; set; }


        [Output("logradouro")]                                               
        public OutArgument<string> logradouro { get; set; }
        [Output("complemento")]
        public OutArgument<string> complemento { get; set; }
        [Output("bairro")]
        public OutArgument<string> bairro { get; set; }
        [Output("localidade")]
        public OutArgument<string> localidade { get; set; }
        [Output("uf")]
        public OutArgument<string> uf { get; set; }
        [Output("ibge")]
        public OutArgument<string> ibge { get; set; }
        [Output("ddd")]
        public OutArgument<string> ddd { get; set; }

        public string Log { get; set; }

        public override void ExecuteAction(CodeActivityContext context)
        {
            GetAddressOnAPI();
        }

        private RestResponse GetAddressOnAPI()
        {
            this.Log += "GetAddressOnAPI";
            var CEP = this.Cep;

            var options = new RestClientOptions($"viacep.com.br/ws/{CEP}/json/");

            var address = new RestClient(options);
            var request = new RestRequest("/account", Method.Post);
            RestResponse response = address.Execute(request);
            return response;
        }

        private AccountAddressVO GetAddressWithCEP(CodeActivityContext context, RestResponse response)
        {
            this.Log += "GetProductWithID";

            this.Log += response.Content;

            AccountAddressVO accountAddressVO = JsonConvert.DeserializeObject<AccountAddressVO>(response.Content);

            logradouro = logradouro.Set();

            this.Log += "Converteu JSON";

            return accountAddressVO;

        }

    }
}