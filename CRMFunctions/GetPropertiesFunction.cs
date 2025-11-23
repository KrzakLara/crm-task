using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace CRM_Property_Functions
{
    public class GetPropertiesFunction
    {
        private readonly GetPropertiesService _service;

        public GetPropertiesFunction(GetPropertiesService service)
        {
            _service = service;
        }

        [OpenApiOperation(operationId: "GetProperties", tags: new[] { "Properties" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<PropertyDto>), Description = "List of active properties")]
        [Function("GetProperties")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
            FunctionContext context)
        {
            var logger = context.GetLogger("GetProperties");
            logger.LogInformation("Fetching properties...");

            try
            {
                var properties = await _service.FetchPropertiesAsync();

                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(properties);
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching properties");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteAsJsonAsync(new { message = ex.Message });
                return errorResponse;
            }
        }
    }
}
