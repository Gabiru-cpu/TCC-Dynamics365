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
        public GetUom GetUom { get; set; }

        public ControllerUom(IOrganizationService crmServiceClient)
        {
            ServiceClient = crmServiceClient;
            this.GetUom = new GetUom(ServiceClient);
        }

        public Entity GetById(Guid id, string[] columns)
        {
            return GetUom.GetById(id, columns);
        }

    }
}
