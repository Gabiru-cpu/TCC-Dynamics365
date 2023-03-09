using DynacoopTccAmbiente2.Logistics.DynacoopTccLogisticsISV;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTccAmbiente2.Logistics.Plugins
{
    public class ProductManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            BlockCreateData(this.Context);
        }
        public void BlockCreateData(IPluginExecutionContext context)
        {
            
            if(context.MessageName == "Create")
            {
                
                Entity entity = (Entity)context.InputParameters["Target"];
                if (entity.Attributes.ContainsKey("atv_verificarimplementacao"))
                {
                    var verify = (bool)entity.Attributes["atv_verificarimplementacao"];
                    
                    if (!verify)
                        throw new InvalidPluginExecutionException("Não é possivel criar um produto no ambiente 2");
                }
                else
                {
                    throw new InvalidPluginExecutionException("Não é possivel criar um produto no ambiente 2");
                }
                
            }
        }
    }
}
