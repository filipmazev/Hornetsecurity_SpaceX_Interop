export class LaunchRow {
    missionPatchImage?: string;
    name: string;
    rocketName: string;
    launchpadName: string;
    launchDateUtc: string;
    upcoming: boolean
    success?: boolean;

    webcastUrl?: string;
    wikipediaUrl?: string;
    articleUrl?: string;

    payloads: number;    

    constructor(
        missionPatchImage: string | undefined,
        name: string,
        rocketName: string,
        launchpadName: string,
        launchDateUtc: string,
        upcoming: boolean,
        success: boolean | undefined,
        webcastUrl: string | undefined,
        wikipediaUrl: string | undefined,
        articleUrl: string | undefined,
        payloads: number
    ) {
        this.missionPatchImage = missionPatchImage;
        this.name = name;
        this.rocketName = rocketName;
        this.launchpadName = launchpadName;
        this.launchDateUtc = launchDateUtc;
        this.upcoming = upcoming;
        this.success = success;
        this.webcastUrl = webcastUrl;
        this.wikipediaUrl = wikipediaUrl;
        this.articleUrl = articleUrl;
        this.payloads = payloads;
    }
}