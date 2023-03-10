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
    public class ContactManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity contactParameters = (Entity)Context.InputParameters["Target"];

            var cpf = contactParameters["dcp_cpf"];

            var query = new QueryExpression("contact");
            query.ColumnSet = new ColumnSet("dcp_cpf");
            query.Criteria.AddCondition("dcp_cpf", ConditionOperator.Equal, cpf);
            var results = Service.RetrieveMultiple(query);          

            if (Context.MessageName == "Create")
            {
                if (results.Entities.Count > 1)
                {
                    throw new InvalidPluginExecutionException("CPF já cadastrado.");
                }

            }           


        }
    }
}
