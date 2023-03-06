using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynacoopTCC.Logistics.Solution
{
    public class Singleton
    {
        private readonly string _url;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly CrmServiceClient _serviceClient;

        public Singleton(string url, string clientId, string clientSecret)
        {
            _url = url;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _serviceClient = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{_url}.crm2.dynamics.com/;AppId={_clientId};ClientSecret={_clientSecret};");
        }

        public IOrganizationService GetService()
        {
            // Cria conexões para os dois ambientes do Dynamics
            //CrmServiceClient environment1Connection = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{environment1Url}.crm2.dynamics.com/;AppId={clientId1};ClientSecret={clientSecret1};");
            //CrmServiceClient environment2Connection = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{environment2Url}.crm2.dynamics.com/;AppId={clientId2};ClientSecret={clientSecret2};");            

            return _serviceClient.OrganizationWebProxyClient != null
            ? (IOrganizationService)_serviceClient.OrganizationWebProxyClient
            : (IOrganizationService)_serviceClient.OrganizationServiceProxy;
        }
    }
}
