using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Dynamics365
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Define as credenciais para o primeiro ambiente do Dynamics
            string environment1Url = "" + "logisticsproducts";
            string environment1Username = "gabrielxavierlibrande@ghxl.onmicrosoft.com";
            string environment1Password = "biel1212.";

            string clientId1 = "c1fa970e-36ff-4ca3-af66-02523e5b0b79";
            string clientSecret1 = "iDt8Q~49_eoxOAG2jNxAaz~v9TqFpT4F7ZE~caiy";

            // Define as credenciais para o segundo ambiente do Dynamics
            string environment2Url = "logisticsplugin";
            string environment2Username = "EduardoGuilherme@AtividadeLtda.onmicrosoft.com";
            string environment2Password = "19960304Ed";

            string clientId2 = "83d4b16a-8f9c-4a05-a9b4-1c55f85e51fa";
            string clientSecret2 = "LrI8Q~fpWCD6WzR61Egceu2Ee_DsdzptztM6bcsc";

            // Cria conexões para os dois ambientes do Dynamics
            var environment1Connection = new CrmServiceClient($"Url={environment1Url};Username={environment1Username};Password={environment1Password}");
            var environment2Connection = new CrmServiceClient($"Url={environment2Url};Username={environment2Username};Password={environment2Password}");

            // Exemplo: criar uma nova entidade no ambiente 1 e copiá-la para o ambiente 2
            var newEntity = new Entity("account");
            newEntity["TESTE1"] = "EMPRESA X";
            environment1Connection.Create(newEntity);
            var copiedEntity = environment1Connection.Retrieve("account", newEntity.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));
            environment2Connection.Create(copiedEntity);

            

            Console.ReadKey();
        }
    }
}
