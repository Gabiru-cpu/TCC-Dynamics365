using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Tooling.Connector;

namespace DynacoopTCC.Logistics.Solution
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // Define as credenciais para o primeiro ambiente do Dynamics
            string environment1Url = "" + "logisticsproducts";
            string clientId1 = "c1fa970e-36ff-4ca3-af66-02523e5b0b79";
            string clientSecret1 = "iDt8Q~49_eoxOAG2jNxAaz~v9TqFpT4F7ZE~caiy";

            // Define as credenciais para o segundo ambiente do Dynamics
            string environment2Url = "logisticsplugin";
            string clientId2 = "83d4b16a-8f9c-4a05-a9b4-1c55f85e51fa";
            string clientSecret2 = "LrI8Q~fpWCD6WzR61Egceu2Ee_DsdzptztM6bcsc";

            // Cria conexões para os dois ambientes do Dynamics
            CrmServiceClient environment1Connection = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{environment1Url}.crm2.dynamics.com/;AppId={clientId1};ClientSecret={clientSecret1};");
            CrmServiceClient environment2Connection = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{environment2Url}.crm2.dynamics.com/;AppId={clientId2};ClientSecret={clientSecret2};");

            // Exemplo: criar uma nova entidade no ambiente 1 e copiá-la para o ambiente 2
            var newEntity = new Entity("account"); //product
            newEntity["name"] = "CONTA NOVA";

            environment1Connection.Create(newEntity);

            var copiedEntity = newEntity;
            environment2Connection.Create(copiedEntity);
        }
    }
}
