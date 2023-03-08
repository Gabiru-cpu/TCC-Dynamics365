using DynacoopTCC.Logistics.Plugin.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.Controller
{
    public class ControllerProduct
    {
        public IOrganizationService ServiceClient { get; set; }
        public GetProduct GetProduct { get; set; }

        public ControllerProduct(IOrganizationService crmServiceClient)
        {
            ServiceClient = crmServiceClient;
            this.GetProduct = new GetProduct(ServiceClient);
        }

        public Entity GetProductById(Guid id, string[] columns)
        {
            return GetProduct.GetProductById(id, columns);
        }

    }
}
