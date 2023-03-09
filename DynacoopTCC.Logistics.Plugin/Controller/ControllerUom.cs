using DynacoopTCC.Logistics.Plugin.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.Controller
{
    public class ControllerUom
    {
        public IOrganizationService ServiceClient { get; set; }
        public GetUom getUom { get; set; }

        public ControllerUom(IOrganizationService crmServiceClient)
        {
            ServiceClient = crmServiceClient;
            this.getUom = new GetUom(ServiceClient);
        }

        public Entity GetById(Guid id, string[] columns)
        {
            return getUom.GetById(id, columns);
        }

    }
}
