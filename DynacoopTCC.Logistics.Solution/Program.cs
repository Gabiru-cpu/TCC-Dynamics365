using DynacoopTCC.Logistics.Plugin.Controller;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Tooling.Connector;
using System.Runtime.Remoting.Contexts;

namespace DynacoopTCC.Logistics.Solution
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Entity productPostImage = new Entity("product");
            productPostImage["name"] = "Fantasia";
            productPostImage["productnumber"] = "PROD-678";
            productPostImage["defaultuomscheduleid"] = new EntityReference("uomschedule", new Guid("db1bbf1d-7a63-4ddd-9ea8-8e533d9ee80b"));
            productPostImage["defaultuomid"] = new EntityReference("uom", new Guid("14e9cecb-c1b7-4e59-9905-f0010ddbe79a"));
            // Define as credenciais para o segundo ambiente do Dynamics
            string environment1Url = "" + "logisticsproducts";
            string clientId1 = "c1fa970e-36ff-4ca3-af66-02523e5b0b79";
            string clientSecret1 = "iDt8Q~49_eoxOAG2jNxAaz~v9TqFpT4F7ZE~caiy";

            CrmServiceClient environment1Connection = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{environment1Url}.crm2.dynamics.com/;AppId={clientId1};ClientSecret={clientSecret1};");

            string environment2Url = "logisticsplugin";
            string clientId2 = "83d4b16a-8f9c-4a05-a9b4-1c55f85e51fa";
            string clientSecret2 = "LrI8Q~fpWCD6WzR61Egceu2Ee_DsdzptztM6bcsc";

            // Cria conexões para os dois ambientes do Dynamics            
            CrmServiceClient environment2Connection = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{environment2Url}.crm2.dynamics.com/;AppId={clientId2};ClientSecret={clientSecret2};");

            // Exemplo: criar uma nova entidade no ambiente 1 e copiá-la para o ambiente 2
            var product = new Entity("product"); //product                        

            product["name"] = productPostImage["name"];
            product["productnumber"] = productPostImage["productnumber"];
            product["defaultuomscheduleid"] = productPostImage["defaultuomscheduleid"];
            product["defaultuomid"] = productPostImage["defaultuomid"];

            product["atv_verificarimplementacao"] = true;

            EntityReference defaultUomScheduleid = (EntityReference)productPostImage["defaultuomscheduleid"];
            
            ControllerUnityGroup controllerUnityGroupDestiny = new ControllerUnityGroup(environment2Connection);
            
            var unityGroup = controllerUnityGroupDestiny.GetById(defaultUomScheduleid.Id, new string[] { "uomscheduleid" });

            if (unityGroup != null)
            {
                product["defaultuomscheduleid"] = productPostImage["defaultuomscheduleid"];                
            }
            else
            {

                ControllerUnityGroup controllerUnityGroupOrigin = new ControllerUnityGroup(environment1Connection);

                var unityGroupOrigin = controllerUnityGroupOrigin.GetById(defaultUomScheduleid.Id, new string[] { "uomscheduleid", "name", "baseuomname" });

                Entity unityGroup1 = new Entity("uomschedule");


                unityGroup1["name"] = unityGroupOrigin["name"] + " teste";

                unityGroup1["uomscheduleid"] = unityGroupOrigin["uomscheduleid"];
                unityGroup1["baseuomname"] = unityGroupOrigin["baseuomname"] + " teste";
                

                environment2Connection.Create(unityGroup1);

            }


            EntityReference defaultuomid = (EntityReference)productPostImage["defaultuomid"];

            ControllerUom controllerUomDestiny = new ControllerUom(environment2Connection);

            var uomGroup = controllerUomDestiny.GetById(defaultuomid.Id, new string[] { "uomid" });

            if (uomGroup != null)
            {
                product["defaultuomid"] = productPostImage["defaultuomid"];
            }
            else
            {
                ControllerUom controllerUomOrigin = new ControllerUom(environment1Connection);
                var unityGroupOrigin = controllerUomOrigin.GetById(defaultuomid.Id, new string[] { "uomid", "name", "quantity", "uomscheduleid" });
                uomGroup = new Entity("uom");

                uomGroup["uomscheduleid"] = unityGroupOrigin["uomscheduleid"];
                uomGroup["uomid"] = unityGroupOrigin["uomid"];
                uomGroup["name"] = unityGroupOrigin["name"];
                uomGroup["quantity"] = unityGroupOrigin["quantity"];

                environment2Connection.Create(uomGroup);
            }

            environment2Connection.Create(product);

        }
    }
}
