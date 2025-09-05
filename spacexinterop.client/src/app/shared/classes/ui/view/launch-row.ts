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
        articleUrl: string | undefined
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
    }
}