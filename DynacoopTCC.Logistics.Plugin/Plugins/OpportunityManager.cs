using DynacoopTCC.Logistics.Plugin.Controller;
using DynacoopTCC.Logistics.Plugin.DynacoopTCC.LogisticsISV;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.Plugins
{
    public class OpportunityManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity opportunity = (Entity)Context.InputParameters["Target"];
            ControllerOpportunity controllerOpportunity = new ControllerOpportunity(this.Service);

            string ticket = controllerOpportunity.CreateOpportunityTicket();

            while (controllerOpportunity.ValidateOpportunityTicket(ticket) != null)
            {
                ticket = controllerOpportunity.CreateOpportunityTicket();
            }

            opportunity.Attributes["dcp_oppticket"] = ticket;
        }
    }
}
