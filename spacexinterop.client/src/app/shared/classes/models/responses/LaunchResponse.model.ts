import { DatePrecisionEnum } from "../../../enums/api/DatePrecisionEnum";
import { PayloadResponse } from "./PayloadResponse.model";

export class LaunchResponse {
    name: string;
    rocketName: string;
    launchpadName: string;
    launchDateUtc: Date;
    datePrecision: DatePrecisionEnum;
    details?: string;
    upcoming: boolean;
    success?: boolean;
    failureReasons: string[] = [];
    missionPatchImage?: string;
    webcastUrl?: string;
    wikipediaUrl?: string;
    articleUrl?: string;

    payloads: PayloadResponse[] = [];

    constructor(
        name: string,
        rocketName: string,
        launchpadName: string,
        launchDateUtc: Date,
        datePrecision: DatePrecisionEnum,
        details: string | undefined,
        upcoming: boolean,
        success: boolean | undefined,
        failureReasons: string[],
        missionPatchImage: string | undefined,
        webcastUrl: string | undefined,
        wikipediaUrl: string | undefined,
        articleUrl: string | undefined,
        payloads: PayloadResponse[]
    ) {
        this.name = name;
        this.rocketName = rocketName;
        this.launchpadName = launchpadName;
        this.launchDateUtc = launchDateUtc;
        this.datePrecision = datePrecision;
        this.details = details;
        this.upcoming = upcoming;
        this.success = success;
        this.failureReasons = failureReasons;
        this.missionPatchImage = missionPatchImage;
        this.webcastUrl = webcastUrl;
        this.wikipediaUrl = wikipediaUrl;
        this.articleUrl = articleUrl;
        this.payloads = payloads;
    }
}