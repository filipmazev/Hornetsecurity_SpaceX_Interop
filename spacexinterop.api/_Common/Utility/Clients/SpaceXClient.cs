using spacexinterop.api._Common.Utility.Clients.Interfaces;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api.Data.Response.External.Space_X;
using spacexinterop.api.Data.Request.External.Space_X;
using spacexinterop.api.Data.Enums.External.Space_X;
using spacexinterop.api._Common.Utility.Extensions;
using System.Text.Json;
using System.Text;

namespace spacexinterop.api._Common.Utility.Clients;

public class SpaceXClient(HttpClient httpClient) : ISpaceXClient
{
    public async Task<SpaceXPaginatedResponse<TModel>?> GetQueryResponse<TModel>(SpaceXQueryRequest queryRequest,
        CancellationToken cancellationToken = default)
        where TModel : BaseJsonModel, new()
    {
        string jsonQueryRequest = JsonSerializer.Serialize(queryRequest, new JsonSerializerOptions { WriteIndented = true });

        TModel dummy = new();

        HttpResponseMessage response = await httpClient.PostAsync(
            $"{dummy.JsonPluralName}/{RequestTypeEnum.Query.GetJsonPropertyName()}",
            new StringContent(jsonQueryRequest, Encoding.UTF8, "application/json"),
            cancellationToken
        );

        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync(cancellationToken);

        SpaceXPaginatedResponse<TModel>? paginatedResponse =
        JsonSerializer.Deserialize<SpaceXPaginatedResponse<TModel>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return paginatedResponse;
    }
}