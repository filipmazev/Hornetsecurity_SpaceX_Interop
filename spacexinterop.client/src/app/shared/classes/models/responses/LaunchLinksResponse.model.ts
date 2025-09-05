import { RedditResponse } from "./RedditResponse.model";

export class LaunchLinksResponse {
    reddit?: RedditResponse;
    flickerImagesSmall: string[] = [];
    flickerImagesOriginal: string[] = [];
    missionPatchImageSmall?: string;
    missionPatchImageOriginal?: string;
    pressKitUrl?: string;
    webcastUrl?: string;
    articleUrl?: string;
    wikipediaUrl?: string;

    constructor(
        reddit: RedditResponse | undefined,
        flickerImagesSmall: string[],
        flickerImagesOriginal: string[],
        missionPatchImageSmall: string | undefined,
        missionPatchImageOriginal: string | undefined,
        pressKitUrl: string | undefined,
        webcastUrl: string | undefined,
        articleUrl: string | undefined,
        wikipediaUrl: string | undefined
    ) {
        this.reddit = reddit;
        this.flickerImagesSmall = flickerImagesSmall;
        this.flickerImagesOriginal = flickerImagesOriginal;
        this.missionPatchImageSmall = missionPatchImageSmall;
        this.missionPatchImageOriginal = missionPatchImageOriginal;
        this.pressKitUrl = pressKitUrl;
        this.webcastUrl = webcastUrl;
        this.articleUrl = articleUrl;
        this.wikipediaUrl = wikipediaUrl;
    }
}
