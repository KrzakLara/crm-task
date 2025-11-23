using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.PowerPlatform.Dataverse.Client;
using Newtonsoft.Json;

public class GetProperties
{
    private readonly DataverseService _dataverse;

    public GetProperties(DataverseService dataverse)
    {
        _dataverse = dataverse;
    }

    [Function("GetProperties")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "properties")] HttpRequestData req)
    {
        var client = _dataverse.CreateClient();

        string query = "/api/data/v9.2/cr63f_properties?$select=cr63f_propertyname,cr63f_location,cr63f_propertytype,cr63f_numberofrooms,cr63f_averagedailyrate,cr63f_rating";

        var response = await client.GetAsync(query);

        var json = await response.Content.ReadAsStringAsync();

        var httpResponse = req.CreateResponse(HttpStatusCode.OK);
        httpResponse.Headers.Add("Content-Type", "application/json");
        await httpResponse.WriteStringAsync(json);

        return httpResponse;
    }
}
