using DynacoopTCC.Logistics.Plugin.Controller;
using DynacoopTCC.Logistics.Plugin.DynacoopTCC.LogisticsISV;
using Microsoft.Rest;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.Plugins
{
    public class AccountManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity accountParameters = (Entity)Context.InputParameters["Target"];

            var cnpj = accountParameters["dcp_cnpj"];
            TracingService.Trace("accountparameters cnpj: " + cnpj);

            var query = new QueryExpression("account");
            query.ColumnSet = new ColumnSet("dcp_cnpj");
            query.Criteria.AddCondition("dcp_cnpj", ConditionOperator.Equal, cnpj);
            var results = Service.RetrieveMultiple(query);          

            if (Context.MessageName == "Create")
            {
                if (results.Entities.Count > 1)
                {
                    throw new InvalidPluginExecutionException("CNPJ já cadastrado.");
                }

            }           


        }
    }
}
