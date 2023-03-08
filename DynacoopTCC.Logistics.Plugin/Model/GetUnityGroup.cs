using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.Model
{
    public class GetUnityGroup
    {
        public IOrganizationService ServiceClient { get; set; }

        public string Logicalname { get; set; }

        public GetUnityGroup(IOrganizationService crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.Logicalname = "uomschedule";
        }

        public Entity GetById(Guid id, string[] columns)
        {
            QueryExpression query = new QueryExpression(this.Logicalname);
            query.ColumnSet.AddColumns(columns);
            query.Criteria.AddCondition("uomscheduleid", ConditionOperator.Equal, id);            

            EntityCollection entityCollection = this.ServiceClient.RetrieveMultiple(query);
            return entityCollection.Entities.FirstOrDefault();
        }

    }
}
