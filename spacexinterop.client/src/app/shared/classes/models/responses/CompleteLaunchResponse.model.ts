import { DatePrecisionEnum } from "../../../enums/api/DatePrecisionEnum";
import { CapsuleResponse } from "./CapsuleResponse.model";
import { LaunchCoreResponse } from "./LaunchCoreResponse.model";
import { LaunchLinksResponse } from "./LaunchLinksResponse.model";
import { PayloadResponse } from "./PayloadResponse.model";
import { ShipResponse } from "./ShipResponse";

export class CompleteLaunchResponse {
    id: string;
    name: string;
    flightNumber: number;
    rocketName: string;
    launchpadName: string;
    staticFireDateUtc: Date;
    launchDateUtc: Date;
    datePrecision: DatePrecisionEnum;
    details?: string;
    upcoming: boolean;
    success?: boolean;
    failureReasons: string[] = [];

    cores: LaunchCoreResponse[] = [];
    payloads: PayloadResponse[] = [];
    ships: ShipResponse[] = [];
    capsules: CapsuleResponse[] = [];

    links?: LaunchLinksResponse;

    constructor(
        id: string,
        name: string,
        flightNumber: number,
        rocketName: string,
        launchpadName: string,
        staticFireDateUtc: Date,
        launchDateUtc: Date,
        datePrecision: DatePrecisionEnum,
        details: string | undefined,
        upcoming: boolean,
        success: boolean | undefined,
        failureReasons: string[],
        cores: LaunchCoreResponse[],
        payloads: PayloadResponse[],
        ships: ShipResponse[],
        capsules: CapsuleResponse[],
        links: LaunchLinksResponse | undefined
    ) {
        this.id = id;
        this.name = name;
        this.flightNumber = flightNumber;
        this.rocketName = rocketName;
        this.launchpadName = launchpadName;
        this.staticFireDateUtc = staticFireDateUtc;
        this.launchDateUtc = launchDateUtc;
        this.datePrecision = datePrecision;
        this.details = details;
        this.upcoming = upcoming;
        this.success = success;
        this.failureReasons = failureReasons;
        this.cores = cores;
        this.payloads = payloads;
        this.ships = ships;
        this.capsules = capsules;
        this.links = links;
    }
}