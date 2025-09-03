export class CoreResponse {
    flight?: number;
    gridFins?: boolean;
    legs?: boolean;
    reused?: boolean;
    landingAttempt?: boolean;
    landingSuccess?: boolean;
    landingType?: string;

    constructor(
        flight: number | undefined,
        gridFins: boolean | undefined,
        legs: boolean | undefined,
        reused: boolean | undefined,
        landingAttempt: boolean | undefined,
        landingSuccess: boolean | undefined,
        landingType: string | undefined
    ) {
        this.flight = flight;
        this.gridFins = gridFins;
        this.legs = legs;
        this.reused = reused;
        this.landingAttempt = landingAttempt;
        this.landingSuccess = landingSuccess;
        this.landingType = landingType;
    }
}