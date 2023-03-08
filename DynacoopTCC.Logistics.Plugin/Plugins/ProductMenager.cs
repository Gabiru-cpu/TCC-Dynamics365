using DynacoopTCC.Logistics.Plugin.Controller;
using DynacoopTCC.Logistics.Plugin.DynacoopTCC.LogisticsISV;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.Plugins
{
    public class ProductMenager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {            
            Entity productPostImage = (Entity)Context.PostEntityImages["PostImage"];

            // Define as credenciais para o segundo ambiente do Dynamics
            string environment2Url = "logisticsplugin";
            string clientId2 = "83d4b16a-8f9c-4a05-a9b4-1c55f85e51fa";
            string clientSecret2 = "LrI8Q~fpWCD6WzR61Egceu2Ee_DsdzptztM6bcsc";

            // Cria conexões para os dois ambientes do Dynamics            
            CrmServiceClient environment2Connection = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{environment2Url}.crm2.dynamics.com/;AppId={clientId2};ClientSecret={clientSecret2};");

            // Exemplo: criar uma nova entidade no ambiente 1 e copiá-la para o ambiente 2
            var product = new Entity("product"); //product                        

            product["name"] = productPostImage["name"];

            product["productnumber"] = productPostImage["productnumber"];

            product["atv_verificarimplementacao"] = true;

            EntityReference defaultUomScheduleid = (EntityReference)productPostImage["defaultuomscheduleid"];
            TracingService.Trace("Passou o EntityReference");
            ControllerUnityGroup controllerUnityGroupDestiny = new ControllerUnityGroup(environment2Connection);
            TracingService.Trace("Passou o Controller");
            var unityGroup = controllerUnityGroupDestiny.GetById(defaultUomScheduleid.Id, new string[] { "uomscheduleid" });

            if(unityGroup != null)
            {
                product["defaultuomscheduleid"] = productPostImage["defaultuomscheduleid"];
                TracingService.Trace("entrou no if");
            }            
            else
            {
                TracingService.Trace("entrou no else");
                ControllerUnityGroup controllerUnityGroupOrigin = new ControllerUnityGroup(Service);
                TracingService.Trace("passou o unitygroupORIGIN");
                var unityGroupOrigin = controllerUnityGroupOrigin.GetById(defaultUomScheduleid.Id, new string[] { "uomscheduleid" , "name" });
                TracingService.Trace("passou o id e o nome");
                Entity unityGroup1 = new Entity("uomschedule");
                TracingService.Trace("passou a entity da tabela");
                
                if(unityGroupOrigin != null)
                    TracingService.Trace("unityGroupOrigin !null");
                else
                    TracingService.Trace("unityGroupOrigin =null");

                TracingService.Trace("default" + defaultUomScheduleid.Id);

                unityGroup1["name"] = unityGroupOrigin["name"];
                TracingService.Trace("passou o valor nm");
                unityGroup1["uomscheduleid"] = unityGroupOrigin["uomscheduleid"];
                TracingService.Trace("passou o valor ID");
                environment2Connection.Create(unityGroup1);
                TracingService.Trace("Criou o produto");
            }
            TracingService.Trace("Passou o 1 if");

            EntityReference defaultuomid = (EntityReference)productPostImage["defaultuomid"];
            TracingService.Trace("Passou o EntityReference2");
            ControllerUom controllerUomDestiny = new ControllerUom(environment2Connection);
            TracingService.Trace("Passou o Controller2");
            var uomGroup = controllerUomDestiny.GetById(defaultuomid.Id, new string[] { "uomid" });

            if (uomGroup != null)
            {
                product["defaultuomid"] = productPostImage["defaultuomid"];
            }
            else
            {
                ControllerUom controllerUomOrigin = new ControllerUom(Service);
                var unityGroupOrigin = controllerUomOrigin.GetById(defaultuomid.Id, new string[] { "uomid", "name", "quantity" });
                uomGroup = new Entity("uom");

                uomGroup["uomid"] = unityGroupOrigin["uomid"];
                uomGroup["name"] = unityGroupOrigin["name"];
                uomGroup["quantity"] = unityGroupOrigin["quantity"];

                environment2Connection.Create(uomGroup);
            }
            TracingService.Trace("Passou o 2 if");
            environment2Connection.Create(product);
            TracingService.Trace("Passou a criacao");
        }
    }
}
