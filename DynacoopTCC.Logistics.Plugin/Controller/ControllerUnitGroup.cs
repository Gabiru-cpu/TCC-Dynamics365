using DynacoopTCC.Logistics.Plugin.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.Controller
{
    public class ControllerUnitGroup
    {
        public IOrganizationService ServiceClient { get; set; }
        public GetUnitGroup getUnityGroup { get; set; }

        public ControllerUnitGroup(IOrganizationService crmServiceClient)
        {
            ServiceClient = crmServiceClient;
            this.getUnityGroup = new GetUnitGroup(ServiceClient);
        }

        public Entity GetById(Guid id, string[] columns)
        {
            return getUnityGroup.GetById(id, columns);
        }

    }
}
