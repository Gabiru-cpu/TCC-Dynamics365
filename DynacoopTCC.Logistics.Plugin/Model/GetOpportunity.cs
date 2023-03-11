using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynacoopTCC.Logistics.Plugin.Controller;

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

        public void IntegrateOpportunity(Entity opportunity, Entity opportunityEnvironment2, IOrganizationService organizationService, ControllerOpportunity controllerOpportunity)
        {

            foreach (var field in opportunity.Attributes)
            {
                if (field.Value != null)
                {
                    if (field.Value.GetType() == new EntityReference().GetType())
                    {
                        if (!controllerOpportunity.getIgnoreFields().ToList().Contains(((EntityReference)field.Value).LogicalName) && field.Key != $"{((EntityReference)field.Value).LogicalName}id")
                            opportunityEnvironment2[field.Key] = controllerOpportunity.ValidateLookup(field, this.ServiceClient, organizationService);
                    }
                    else
                    {
                        if (field.Key != "dcp_oppticket")
                            opportunityEnvironment2[field.Key] = field.Value;
                        else if (field.Key == "dcp_oppticket")
                        {
                            opportunityEnvironment2["dcp2_env1opp"] = true;
                        }
                    }
                }

            }

            organizationService.Create(opportunityEnvironment2);
        }

        public EntityReference ValidateLookup(KeyValuePair<string, object> value, IOrganizationService service, IOrganizationService service2)
        {
            var entityReference = (EntityReference)value.Value;
            var createdEntityId = CreateAndGetEntity(entityReference, service, service2);

            return new EntityReference(entityReference.LogicalName, createdEntityId);
        }

        public Guid CreateAndGetEntity(EntityReference entityReference, IOrganizationService service, IOrganizationService service2)
        {
            var entity = service.Retrieve(entityReference.LogicalName, entityReference.Id, new ColumnSet(getEntityColumns(entityReference.LogicalName)));

            var entityExists = ValidateDuplicate(entity, service2);

            if (entityExists == Guid.Empty)
            {
                var IntegrationEntity = new Entity(entityReference.LogicalName);

                foreach (var field in entity.Attributes)
                {
                    if (field.Value != null)
                    {
                        if (field.Value.GetType() == new EntityReference().GetType())
                        {
                            if (!getIgnoreFields().ToList().Contains(((EntityReference)field.Value).LogicalName) && field.Key != $"{((EntityReference)field.Value).LogicalName}id")
                                IntegrationEntity[field.Key] = ValidateLookup(field, service, service2);
                        }
                        else
                        {
                            IntegrationEntity[field.Key] = field.Value;
                        }
                    }
                }

                var guid = service2.Create(IntegrationEntity);

                return guid;
            }

            return entityExists;
        }

        public Guid ValidateDuplicate(Entity entity, IOrganizationService service)
        {
            QueryExpression query = new QueryExpression(entity.LogicalName);
            query.Criteria.AddCondition(getNameField(entity.LogicalName), ConditionOperator.Equal, entity[getNameField(entity.LogicalName)]);

            var result = service.RetrieveMultiple(query).Entities;

            if (result.Count > 0)
                return result[0].Id;

            return default(Guid);
        }

        public string getNameField(string logicalName)
        {
            switch (logicalName)
            {
                case "account":
                case "pricelevel":
                case "uom":
                case "uomschedule":
                    return "name";

                case "contact":
                    return "fullname";

                case "transactioncurrency":
                    return "currencycode";

                default:
                    return "name";
            }
        }

        public string[] getEntityColumns(string entityName)
        {
            switch (entityName)
            {
                case "account":
                    return new string[] { "name", "telephone1", "fax", "websiteurl", "parentaccountid", "tickersymbol", "address1_line1", "defaultpricelevelid" };

                case "pricelevel":
                    return new string[] { "name", "begindate", "enddate", "transactioncurrencyid" };

                case "uom":
                    return new string[] { "name", "uomscheduleid", "quantity", "baseuom" };

                case "uomschedule":
                    return new string[] { "name", "description" };

                case "contact":
                    return new string[] { "fullname", "firstname", "lastname", "jobtitle", "parentcustomerid", "emailaddress1", "telephone1", "mobilephone",
                                          "fax", "preferredcontactmethodcode", "address1_stateorprovince", "address1_city", "address1_country",
                                          "address1_postalcode", "address1_line1" };

                case "transactioncurrency":
                    return new string[] { "currencyname", "currencycode", "currencyprecision", "currencysymbol", "exchangerate" };

                case "opportunity":
                    return new string[] { "name", "parentcontactid", "parentaccountid", "purchasetimeframe", "transactioncurrencyid", "budgetamount",
                                          "purchaseprocess", "description", "msdyn_forecastcategory", "currentsituation", "customerneed", "proposedsolution" };

                default:
                    return new string[] { "name" };
            }
        }

        public string[] getIgnoreFields()
        {
            return new string[] { "systemuser", "organization", "team", "businessunit", "msdyn_predictivescore", "createdon", "createdby", "modifiedon", "modifiedby", "ownerid" };
        }
    }
}
