using spacexinterop.api._Common.Utility.Mapper.Interfaces;
using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api.Data.Response;

namespace spacexinterop.api._Common.Utility.Mapper;

public class MappingConfig(IMapper mapper) 
    : IMappingConfig
{
    public void RegisterMappings()
    {
        MappingProfile<Launch, LaunchResponse> launchToLaunchResponse =
            new MappingProfile<Launch, LaunchResponse>()
                .ForProperty(src => src.Name, dest => dest.Name);

        mapper.AddProfile(launchToLaunchResponse);
    }
}