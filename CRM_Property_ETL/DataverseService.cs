using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Extensions.Configuration;

public class DataverseService
{
    private readonly IConfiguration _config;

    public DataverseService(IConfiguration config)
    {
        _config = config;
    }

    public ServiceClient CreateClient()
    {
        var url = _config["Dataverse:Url"];
        var clientId = _config["Dataverse:ClientId"];
        var clientSecret = _config["Dataverse:ClientSecret"];
        var tenantId = _config["Dataverse:TenantId"];

        string connString =
            $@"AuthType=ClientSecret;
            Url={url};
            ClientId={clientId};
            ClientSecret={clientSecret};
            TenantId={tenantId};";

        return new ServiceClient(connString);
    }
}
