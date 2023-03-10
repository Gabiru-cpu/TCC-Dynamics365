using DynacoopTCC.Logistics.Plugin.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.Controller
{
    public class ControllerOpportunity
    {
        public IOrganizationService ServiceClient { get; set; }
        public GetOpportunity GetOpportunity { get; set; }

        public ControllerOpportunity(IOrganizationService crmServiceClient)
        {
            ServiceClient = crmServiceClient;
            this.GetOpportunity = new GetOpportunity(ServiceClient);
        }

        public Entity ValidateOpportunityTicket(string ticket)
        {
            return GetOpportunity.ValidateOpportunityTicket(ticket);
        }

        public string CreateOpportunityTicket()
        {
            return GetOpportunity.CreateOpportunityTicket();
        }
    }
}
