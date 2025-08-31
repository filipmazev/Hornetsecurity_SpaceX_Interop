using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api._Common.Utility.Mapper.Interfaces;
using spacexinterop.api.Data.Response;

namespace spacexinterop.api._Common.Utility.Mapper;

public class MappingConfig(IMapper mapper) : IMappingConfig
{
    public void RegisterMappings()
    {
        MappingProfile<Launch, LaunchResponse> launchToLaunchResponse =
            new MappingProfile<Launch, LaunchResponse>()
                .ForProperty(src => src.Name, dest => dest.Name)
                .ForProperty(src => src.Rocket, dest => dest.RocketName, src => src?.Object?.Name ?? string.Empty)
                .ForProperty(src => src.Launchpad, dest => dest.LaunchpadName,
                    src => src?.Object?.FullName ?? string.Empty)

                .ForProperty(src => src.DateUtc, dest => dest.LaunchDateUtc)
                .ForProperty(src => src.DatePrecision, dest => dest.DatePrecision)

                .ForProperty(src => src.Details, dest => dest.Details)
                .ForProperty(src => src.Upcoming, dest => dest.Upcoming)
                .ForProperty(src => src.Success, dest => dest.Success)
                .ForProperty(src => src.Failures, dest => dest.FailureReasons, 
                    src => src.Select(item => item.Reason ?? string.Empty).ToList())

                .ForProperty(src => src.Links, dest => dest.MissionPatchImage, src => src?.Patch?.Small)
                .ForProperty(src => src.Links, dest => dest.WebcastUrl, src => src?.Webcast)
                .ForProperty(src => src.Links, dest => dest.WikipediaUrl, src => src?.Wikipedia)
                .ForProperty(src => src.Links, dest => dest.ArticleUrl, src => src?.Article)

                .ForProperty(src => src.Payloads, dest => dest.Payloads, src => src
                    .Select(item => item.Object)
                    .Where(item => item is not null)
                    .Select(item => new PayloadResponse
                {
                    Name = item!.Name,
                    Type = item.Type,
                    Reused = item.Reused,
                    Customers = item.Customers,
                    Manufacturers = item.Manufacturers
                }).ToList());

        mapper.AddProfile(launchToLaunchResponse);
    }
}