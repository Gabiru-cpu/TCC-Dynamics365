using DynacoopTCC.Logistics.Plugin.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.Controller
{
    public class ControllerAccount
    {
        public IOrganizationService ServiceClient { get; set; }
        public GetAccount GetAccount { get; set; }

        public ControllerAccount(IOrganizationService crmServiceClient)
        {
            ServiceClient = crmServiceClient;
            this.GetAccount = new GetAccount(ServiceClient);
        }

        public Entity GetAccountById(Guid id, string[] columns)
        {
            return GetAccount.GetAccountById(id, columns);
        }

    }
}
