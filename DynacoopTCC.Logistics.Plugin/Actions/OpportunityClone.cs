using DynacoopTCC.Logistics.Plugin.VO;
using DynacoopTCC.Logistics.Plugin.DynacoopTCC.LogisticsISV;
using Microsoft.Xrm.Sdk.Workflow;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using System.Web.Services.Description;
using DynacoopTCC.Logistics.Plugin.Controller;
using System.Web.UI.WebControls;
using Microsoft.Xrm.Sdk.Query;

namespace DynacoopTCC.Logistics.Plugin.Actions
{
    public class OpportunityClone : ActionCore
    {
        [Input("idOpotunityOrigin")]
        public InArgument<Guid> idOpotunityOrigin { get; set; }


        [Output("idOportunityDestiny")]
        public OutArgument<string> idOportunityDestiny { get; set; }

        public string Log { get; set; }

        public override void ExecuteAction(CodeActivityContext context)
        {
            GetCloneOpportunity(context);
        }



        private void GetCloneOpportunity(CodeActivityContext context)
        {
            var newOpportunityClone = new Entity("opportunityclone");

            var entityId = idOpotunityOrigin.Get(context);
            var columns = new ColumnSet(true);

            Entity entity = this.Service.Retrieve("opportunity", entityId, columns);
            newOpportunityClone["name"] = entity["name"];
            newOpportunityClone["transactioncurrencyid"] = entity["transactioncurrencyid"];
            Service.Create(newOpportunityClone);
        }      

    }
}