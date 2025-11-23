using Xunit;
using System.Net.Http;
using System.Threading.Tasks;

public class GetPropertiesIntegrationTests
{
    private readonly HttpClient _client = new HttpClient();

    [Fact]
    public async Task GetProperties_ReturnsSuccessAndJson()
    {
        var response = await _client.GetAsync("http://localhost:7046/api/GetProperties");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Assert.Contains("Name", json);
        Assert.Contains("Location", json);
    }
}
