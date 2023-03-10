using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.Model
{
    public class GetOpportunity
    {
        public IOrganizationService ServiceClient { get; set; }

        public string Logicalname { get; set; }

        public GetOpportunity(IOrganizationService crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.Logicalname = "opportunity";
        }

        public Entity ValidateOpportunityTicket(string ticket)
        {
            QueryExpression query = new QueryExpression(this.Logicalname);
            query.Criteria.AddCondition("dcp_oppticket", ConditionOperator.Equal, ticket);

            return this.ServiceClient.RetrieveMultiple(query).Entities.FirstOrDefault();
        }

        public string CreateOpportunityTicket()
        {
            Random rnd = new Random();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            char letter1 = letters[rnd.Next(letters.Length)];
            char letter2 = letter1;

            while (letter2 == letter1)
            {
                letter2 = letters[rnd.Next(letters.Length)];
            }

            return string.Format("OPP-" + rnd.Next(1, 9) + rnd.Next(0, 9) + rnd.Next(0, 9) + rnd.Next(0, 9) + rnd.Next(0, 9) + "-" + letter1 + rnd.Next(1, 9) + letter2 + rnd.Next(1, 9));
        }
    }
}
