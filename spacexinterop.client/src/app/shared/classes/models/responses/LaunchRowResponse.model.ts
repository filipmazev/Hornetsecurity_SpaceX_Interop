import { DatePrecisionEnum } from "../../../enums/api/DatePrecisionEnum";
import { LaunchLinksResponse } from "./LaunchLinksResponse.model";

export class LaunchRowResponse {
    id: string;
    name: string;
    rocketName: string;
    launchpadName: string;
    launchDateUtc: Date;
    datePrecision: DatePrecisionEnum;
    details?: string;
    upcoming: boolean;
    success?: boolean;
    failureReasons: string[] = [];
    links?: LaunchLinksResponse;

    constructor(
        id: string,
        name: string,
        rocketName: string,
        launchpadName: string,
        launchDateUtc: Date,
        datePrecision: DatePrecisionEnum,
        details: string | undefined,
        upcoming: boolean,
        success: boolean | undefined,
        failureReasons: string[],
        links: LaunchLinksResponse | undefined
    ) {
        this.id = id;
        this.name = name;
        this.rocketName = rocketName;
        this.launchpadName = launchpadName;
        this.launchDateUtc = launchDateUtc;
        this.datePrecision = datePrecision;
        this.details = details;
        this.upcoming = upcoming;
        this.success = success;
        this.failureReasons = failureReasons;
        this.links = links;
    }
}