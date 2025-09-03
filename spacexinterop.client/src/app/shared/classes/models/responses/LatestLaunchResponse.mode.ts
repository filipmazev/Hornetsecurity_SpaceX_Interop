import { DatePrecisionEnum } from "../../../enums/api/DatePrecisionEnum";
import { CoreResponse } from "./CoreResponse.model";
import { RedditResponse } from "./RedditResponse.model";

export class LatestLaunchResponse {
    name: string;
    flightNumber: number;

    staticFireDateUtc: Date;
    launchDateUtc: Date;
    datePrecision: DatePrecisionEnum;

    details?: string;
    upcoming: boolean;
    success?: boolean;

    failureReasons: string[] = [];
    cores: CoreResponse[] = [];

    reddit?: RedditResponse;

    flickerImagesOriginal: string[] = [];

    missionPatchImageSmall?: string;
    missionPatchImageOriginal?: string;

    pressKitUrl?: string;
    webcastUrl?: string;
    articleUrl?: string;
    wikipediaUrl?: string;

    constructor(
        name: string,
        flightNumber: number,
        staticFireDateUtc: Date,
        launchDateUtc: Date,
        datePrecision: DatePrecisionEnum,
        details: string | undefined,
        upcoming: boolean,
        success: boolean | undefined,
        failureReasons: string[],
        cores: CoreResponse[],
        reddit: RedditResponse | undefined,
        flickerImagesOriginal: string[],
        missionPatchImageSmall: string | undefined,
        missionPatchImageOriginal: string | undefined,
        pressKitUrl: string | undefined,
        webcastUrl: string | undefined,
        articleUrl: string | undefined,
        wikipediaUrl: string | undefined
    ) {
        this.name = name;
        this.flightNumber = flightNumber;
        this.staticFireDateUtc = staticFireDateUtc;
        this.launchDateUtc = launchDateUtc;
        this.datePrecision = datePrecision;
        this.details = details;
        this.upcoming = upcoming;
        this.success = success;
        this.failureReasons = failureReasons;
        this.cores = cores;
        this.reddit = reddit;
        this.flickerImagesOriginal = flickerImagesOriginal;
        this.missionPatchImageSmall = missionPatchImageSmall;
        this.missionPatchImageOriginal = missionPatchImageOriginal;
        this.pressKitUrl = pressKitUrl;
        this.webcastUrl = webcastUrl;
        this.articleUrl = articleUrl;
        this.wikipediaUrl = wikipediaUrl;
    }
}