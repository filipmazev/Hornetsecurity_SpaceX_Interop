using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api.Data.Models.External.Space_X.Rockets;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api.Data.Models.External.Space_X;
using spacexinterop.api.Data.Enums.External.Space_X;

namespace spacexinterop.api.Repositories;

public partial class SpaceXLaunchesRepository
{
    private readonly PopulateOption _populateRocketOption = PopulateOption.With<Launch, GuidOrObject<Rocket>>(launch => launch.Rocket!)
        .Selecting<Rocket, string>(rocket => rocket.Name);

    private readonly PopulateOption _populateLaunchpadOption = PopulateOption.With<Launch, GuidOrObject<Launchpad>>(launch => launch.Launchpad!)
        .Selecting<Launchpad, string?>(launchpad => launchpad.FullName);
    
    private readonly PopulateOption _populatePayloadsOption = PopulateOption
        .With<Launch, List<GuidOrObject<Payload>>>(launch => launch.Payloads)
        .Selecting<Payload, string?>(payload => payload.Name)
        .Selecting<Payload, string?>(payload => payload.Type)
        .Selecting<Payload, bool>(payload => payload.Reused)
        .Selecting<Payload, List<string>>(payload => payload.Customers)
        .Selecting<Payload, List<string>>(payload => payload.Manufacturers);

    private readonly PopulateOption _populateLaunchCoresOption = PopulateOption
        .With<Launch, List<LaunchCore>>(launch => launch.Cores)
        .Selecting<LaunchCore, GuidOrObject<Core>?>(item => item.Core)
        .PopulateNested<LaunchCore, GuidOrObject<Core>>(launchCore => launchCore.Core!, option => option
            .Selecting<Core, string>(core => core.Serial)
            .Selecting<Core, int?>(core => core.Block)
            .Selecting<Core, CoreStatusEnum>(core => core.Status)
            .Selecting<Core, int>(core => core.ReuseCount)
            .Selecting<Core, string?>(core => core.LastUpdate));

    private readonly PopulateOption _populateShipsOption = PopulateOption
        .With<Launch, List<GuidOrObject<Ship>>>(launch => launch.Ships)
            .Selecting<Ship, string>(ship => ship.Name)
            .Selecting<Ship, string?>(ship => ship.Type)
            .Selecting<Ship, string?>(ship => ship.Image);

    private readonly PopulateOption _populateCapsuleOption = PopulateOption
        .With<Launch, List<GuidOrObject<Capsule>>>(launch => launch.Capsules)
            .Selecting<Capsule, string>(capsule => capsule.Serial)
            .Selecting<Capsule, string>(capsule => capsule.Status)
            .Selecting<Capsule, string>(capsule => capsule.Type)
            .Selecting<Capsule, int>(capsule => capsule.ReuseCount)
            .Selecting<Capsule, int>(capsule => capsule.WaterLandings);
}