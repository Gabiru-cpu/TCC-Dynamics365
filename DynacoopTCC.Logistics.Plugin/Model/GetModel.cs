﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.Model
{
    internal class GetModel
    {
        public IOrganizationService ServiceClient { get; set; }

        public string Logicalname { get; set; }

        public GetModel(IOrganizationService crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.Logicalname = "product";
        }


        public Entity GetProductById(Guid id, string[] columns)
        {
            return ServiceClient.Retrieve(Logicalname, id, new ColumnSet(columns));
        }
    }
}
