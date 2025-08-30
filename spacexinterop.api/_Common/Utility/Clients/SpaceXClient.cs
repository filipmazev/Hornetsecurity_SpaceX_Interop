using spacexinterop.api._Common.Utility.Clients.Interfaces;
using spacexinterop.api.Data.External.Space_X.Launches;
using spacexinterop.api.Data.Request;
using spacexinterop.api.Data.Enums;
using System.Text.Json;

namespace spacexinterop.api._Common.Utility.Clients;

public class SpaceXClient(HttpClient httpClient) : ISpaceXClient
{
    public async Task<List<Launch>> GetLaunches(SpaceXLaunchesRequest request, CancellationToken cancellationToken = default)
    {
        string launchesRequestType = request.LaunchesRequestType switch
        {
            SpaceXLaunchesRequestTypeEnum.Upcoming => "upcoming",
            SpaceXLaunchesRequestTypeEnum.Past => "past",
            SpaceXLaunchesRequestTypeEnum.Latest => "latest",
            _ => throw new ArgumentException("Invalid launches request type.")
        };

        HttpResponseMessage response = await httpClient.GetAsync($"launches/{launchesRequestType}", cancellationToken);
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync(cancellationToken);

        List<Launch>? launches = JsonSerializer.Deserialize<List<Launch>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return launches ?? [];
    }
}