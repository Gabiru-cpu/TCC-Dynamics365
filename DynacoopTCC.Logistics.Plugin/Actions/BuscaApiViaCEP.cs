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
        public InArgument<string> cep { get; set; }


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
            GetAddressOnAPI(context);            
        }

        private void GetAddressOnAPI(CodeActivityContext context)
        {
            this.Log += "GetAddressOnAPI";
            
            var options = new RestClientOptions($"https://viacep.com.br");

            var address = new RestClient(options);

            var tracingCep = this.cep.Get(context);
            TracingService.Trace("https://viacep.com.br/ws/" + tracingCep + "/json/");
            var request = new RestRequest($"/ws/{this.cep.Get(context)}/json/", Method.Get);
            RestResponse response = address.Execute(request);
                        
            GetAddressWithCEP(context, response);
        }

        private void GetAddressWithCEP(CodeActivityContext context, RestResponse response)
        {
            this.Log += "GetProductWithID";

            this.Log += response.Content;
            TracingService.Trace("Antes do DeserializeObject" + response.Content);
            AccountAddressVO accountAddressVO = JsonConvert.DeserializeObject<AccountAddressVO>(response.Content);

            logradouro.Set(context, accountAddressVO.logradouro);
            complemento.Set(context, accountAddressVO.complemento);
            bairro.Set(context, accountAddressVO.bairro);
            localidade.Set(context, accountAddressVO.localidade);
            uf.Set(context, accountAddressVO.uf);
            ibge.Set(context, accountAddressVO.ibge);
            ddd.Set(context, accountAddressVO.ddd);
            
            this.Log += "Converteu JSON";
            
        }

    }
}