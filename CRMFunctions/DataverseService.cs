using Microsoft.Extensions.Configuration;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace CRM_Property_Functions
{
    public class DataverseService
    {
        private readonly IConfiguration _config;

        public DataverseService(IConfiguration config)
        {
            _config = config;
        }

        public ServiceClient GetClient()
        {
            var dv = _config.GetSection("Dataverse");

            string url = dv["Url"];
            string tenant = dv["TenantId"];
            string clientId = dv["ClientId"];
            string clientSecret = dv["ClientSecret"];

            if (string.IsNullOrWhiteSpace(url) ||
                string.IsNullOrWhiteSpace(tenant) ||
                string.IsNullOrWhiteSpace(clientId) ||
                string.IsNullOrWhiteSpace(clientSecret))
            {
                throw new Exception("Missing Dataverse S2S settings (Url / TenantId / ClientId / ClientSecret).");
            }

            string connectionString =
                $"AuthType=ClientSecret;" +
                $"Url={url};" +
                $"ClientId={clientId};" +
                $"ClientSecret={clientSecret};" +
                $"TenantId={tenant};";

            return new ServiceClient(connectionString);
        }
    }
}
