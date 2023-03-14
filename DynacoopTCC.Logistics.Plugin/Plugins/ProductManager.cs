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
    public class ProductManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity productPostImage = (Entity)Context.PostEntityImages["PostImage"];

            string environment1Url = "logisticsproducts";
            string clientId1 = "c1fa970e-36ff-4ca3-af66-02523e5b0b79";
            string clientSecret1 = "iDt8Q~49_eoxOAG2jNxAaz~v9TqFpT4F7ZE~caiy";

            CrmServiceClient environment1Connection = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{environment1Url}.crm2.dynamics.com/;AppId={clientId1};ClientSecret={clientSecret1};");
            IOrganizationService environment2Connection = Environment2Service();

            var product = new Entity("product");

            product["name"] = productPostImage["name"];
            product["productnumber"] = productPostImage["productnumber"];
            product["defaultuomscheduleid"] = productPostImage["defaultuomscheduleid"];
            product["defaultuomid"] = productPostImage["defaultuomid"];

            product["atv_verificarimplementacao"] = true;

            EntityReference defaultUomScheduleid = (EntityReference)productPostImage["defaultuomscheduleid"];

            ControllerUnitGroup controllerUnityGroupDestiny = new ControllerUnitGroup(environment2Connection);

            var unityGroup = controllerUnityGroupDestiny.GetById(defaultUomScheduleid.Id, new string[] { "uomscheduleid" });

            if (unityGroup != null)
            {
                product["defaultuomscheduleid"] = productPostImage["defaultuomscheduleid"];
            }
            else
            {

                ControllerUnitGroup controllerUnityGroupOrigin = new ControllerUnitGroup(environment1Connection);

                var unityGroupOrigin = controllerUnityGroupOrigin.GetById(defaultUomScheduleid.Id, new string[] { "uomscheduleid", "name", "baseuomname" });

                Entity unityGroup1 = new Entity("uomschedule");


                unityGroup["name"] = unityGroupOrigin["name"] + " teste";

                unityGroup["uomscheduleid"] = unityGroupOrigin["uomscheduleid"];
                unityGroup["baseuomname"] = unityGroupOrigin["baseuomname"] + " teste";


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

        public static IOrganizationService Environment2Service()
        {
            string environment2Url = "logisticsplugin";
            string clientId2 = "83d4b16a-8f9c-4a05-a9b4-1c55f85e51fa";
            string clientSecret2 = "LrI8Q~fpWCD6WzR61Egceu2Ee_DsdzptztM6bcsc";

            CrmServiceClient environment2Connection = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{environment2Url}.crm2.dynamics.com/;AppId={clientId2};ClientSecret={clientSecret2};");

            return environment2Connection;
        }
    }
}
