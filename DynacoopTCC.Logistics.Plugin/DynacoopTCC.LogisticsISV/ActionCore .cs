using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Sdk;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Plugin.DynacoopTCC.LogisticsISV
{
    public abstract class ActionCore : CodeActivity
    {
        public IWorkflowContext WorkflowContext { get; set; }
        public IOrganizationServiceFactory ServiceFactory { get; set; }
        public IOrganizationService Service { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            this.WorkflowContext = context.GetExtension<IWorkflowContext>();
            this.ServiceFactory = context.GetExtension<IOrganizationServiceFactory>();
            Service = this.ServiceFactory.CreateOrganizationService(WorkflowContext.UserId);

            ExecuteAction(context);
        }

        public abstract void ExecuteAction(CodeActivityContext context);
    }
}
