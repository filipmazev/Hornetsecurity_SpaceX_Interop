using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api._Common.Utility.Mapper.Interfaces;
using spacexinterop.api.Data.Response;

namespace spacexinterop.api._Common.Utility.Mapper;

public class MappingConfig(IMapper mapper) : IMappingConfig
{
    public void RegisterMappings()
    {
        #region SpaceX

        #region Launches

        #region Complete Launch

        MappingProfile<Launch, CompleteLaunchResponse> launchToCompleteLaunchResponse =
            new MappingProfile<Launch, CompleteLaunchResponse>()
            .ForProperty(src => src.Id, dest => dest.Id)
            
            .ForProperty(src => src.Name, dest => dest.Name)
            .ForProperty(src => src.FlightNumber, dest => dest.FlightNumber)
            .ForProperty(src => src.Rocket, dest => dest.RocketName, src => src?.Object?.Name ?? string.Empty)
            .ForProperty(src => src.Launchpad, dest => dest.LaunchpadName,
                src => src?.Object?.FullName ?? string.Empty)

            .ForProperty(src => src.StaticFireDateUtc, dest => dest.StaticFireDateUtc)
            .ForProperty(src => src.DateUtc, dest => dest.LaunchDateUtc)
            .ForProperty(src => src.DatePrecision, dest => dest.DatePrecision)

            .ForProperty(src => src.Details, dest => dest.Details)
            .ForProperty(src => src.Upcoming, dest => dest.Upcoming)
            .ForProperty(src => src.Success, dest => dest.Success)
            .ForProperty(src => src.Failures, dest => dest.FailureReasons,
                src => src.Select(item => item.Reason ?? string.Empty).ToList())

            .ForProperty(src => src.Links, dest => dest.Links, src =>
            {
                return src != null
                    ? new LaunchLinksResponse
                    {
                        Reddit = src.Reddit != null
                            ? new RedditResponse
                            {
                                CampaignUrl = src.Reddit.Campaign,
                                LaunchUrl = src.Reddit.Launch,
                                MediaUrl = src.Reddit.Media,
                                RecoveryUrl = src.Reddit.Recovery
                            }
                            : null,
                        FlickerImagesSmall = src.Flickr?.Small ?? [],
                        FlickerImagesOriginal = src.Flickr?.Small ?? [],
                        MissionPatchImageSmall = src.Patch?.Small,
                        MissionPatchImageOriginal = src.Patch?.Large,
                        PressKitUrl = src.Presskit,
                        WebcastUrl = src.Webcast,
                        ArticleUrl = src.Article,
                        WikipediaUrl = src.Wikipedia
                    }
                    : null;
            })

            .ForProperty(src => src.Cores, dest => dest.Cores, src => src
                .Select(item => new LaunchCoreResponse
                {
                    Core = item.Core?.Object != null
                        ? new CoreResponse
                        {
                            Serial = item.Core.Object.Serial,
                            Block = item.Core.Object.Block,
                            Status = item.Core.Object.Status,
                            ReuseCount = item.Core.Object.ReuseCount,
                            LastUpdate = item.Core.Object.LastUpdate
                        }
                        : null,
                    Flight = item.FlightNumber,
                    GridFins = item.Gridfins,
                    Legs = item.Legs,
                    Reused = item.Reused,
                    LandingAttempt = item.LandingAttempt,
                    LandingSuccess = item.LandingSuccess,
                    LandingType = item.LandingType
                }).ToList())

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
                }).ToList())
            
            .ForProperty(src => src.Ships, dest => dest.Ships, src => src
                .Select(item => item.Object)
                .Where(item => item is not null)
                .Select(item => new ShipResponse()
                {
                    Name = item!.Name,
                    Type = item.Type,
                    Image = item.Image
                }).ToList())
            
            .ForProperty(src => src.Capsules, dest => dest.Capsules, src => src
                .Select(item => item.Object)
                .Where(item => item is not null)
                .Select(item => new CapsuleResponse()
                {
                    Serial = item!.Serial,
                    Status = item.Status,
                    Type = item.Type,
                    ReuseCount = item.ReuseCount,
                    WaterLandings = item.WaterLandings
                }).ToList());

        mapper.AddProfile(launchToCompleteLaunchResponse);

        #endregion

        #region Launch Row
        
        MappingProfile<Launch, LaunchRowResponse> launchToLaunchRowResponse =
            new MappingProfile<Launch, LaunchRowResponse>()
            .ForProperty(src => src.Id, dest => dest.Id)
            
            .ForProperty(src => src.Name, dest => dest.Name)
            .ForProperty(src => src.FlightNumber, dest => dest.FlightNumber)
            .ForProperty(src => src.Rocket, dest => dest.RocketName, src => src?.Object?.Name ?? string.Empty)
            .ForProperty(src => src.Launchpad, dest => dest.LaunchpadName,
                src => src?.Object?.FullName ?? string.Empty)

            .ForProperty(src => src.StaticFireDateUtc, dest => dest.StaticFireDateUtc)
            .ForProperty(src => src.DateUtc, dest => dest.LaunchDateUtc)
            .ForProperty(src => src.DatePrecision, dest => dest.DatePrecision)

            .ForProperty(src => src.Details, dest => dest.Details)
            .ForProperty(src => src.Upcoming, dest => dest.Upcoming)
            .ForProperty(src => src.Success, dest => dest.Success)
            .ForProperty(src => src.Failures, dest => dest.FailureReasons,
                src => src.Select(item => item.Reason ?? string.Empty).ToList())

            .ForProperty(src => src.Links, dest => dest.Links, src =>
            {
                return src != null
                    ? new LaunchLinksResponse
                    {
                        Reddit = src.Reddit != null
                            ? new RedditResponse
                            {
                                CampaignUrl = src.Reddit.Campaign,
                                LaunchUrl = src.Reddit.Launch,
                                MediaUrl = src.Reddit.Media,
                                RecoveryUrl = src.Reddit.Recovery
                            }
                            : null,
                        FlickerImagesSmall = src.Flickr?.Small ?? [],
                        FlickerImagesOriginal = src.Flickr?.Small ?? [],
                        MissionPatchImageSmall = src.Patch?.Small,
                        MissionPatchImageOriginal = src.Patch?.Large,
                        PressKitUrl = src.Presskit,
                        WebcastUrl = src.Webcast,
                        ArticleUrl = src.Article,
                        WikipediaUrl = src.Wikipedia
                    }
                    : null;
            });

        mapper.AddProfile(launchToLaunchRowResponse);

        #endregion

        #endregion

        #endregion
    }
}