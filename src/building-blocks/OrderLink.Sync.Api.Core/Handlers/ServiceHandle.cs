using OrderLink.Sync.Core.Extensions;
using System.Text;
using System.Text.Json;

namespace OrderLink.Sync.Api.Core.Handlers;

public abstract class ServiceHandle
{
    protected static StringContent GetContent(object dado)
    {
        var testc = JsonSerializer.Serialize(dado);
        return new StringContent(testc, Encoding.UTF8, "application/json");
    }

    protected static async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false
        };

        return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
    }

    protected static bool HandleErrorResponse(HttpResponseMessage response)
    {
        switch ((int)response.StatusCode)
        {
            case 401:
            case 403:
            case 404:
            case 500:
                throw new CustomHttpRequestException(response.StatusCode);

            case 400:
                return false;
        }

        response.EnsureSuccessStatusCode();
        return true;
    }
}
