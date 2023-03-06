using DynacoopTCC.Logistics.Plugin.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.Controller
{
    internal class ControllerModel
    {
        public IOrganizationService ServiceClient { get; set; }
        public GetModel GetModel { get; set; }

        public ControllerModel(IOrganizationService crmServiceClient)
        {
            ServiceClient = crmServiceClient;
            this.GetModel = new GetModel(ServiceClient);
        }

        public Entity GetProductById(Guid id, string[] columns)
        {
            return GetModel.GetProductById(id, columns);
        }

    }
}
