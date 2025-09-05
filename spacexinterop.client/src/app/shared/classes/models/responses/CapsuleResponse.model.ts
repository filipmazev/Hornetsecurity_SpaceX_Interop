import { EMPTY_STRING } from "../../../constants/common.constants";


export class CapsuleResponse {
    serial: string = EMPTY_STRING;
    status: string = EMPTY_STRING;
    type: string = EMPTY_STRING;
    reuseCount: number = 0;
    waterLandings: number = 0;

    constructor(
        serial: string,
        status: string,
        type: string,
        reuseCount: number,
        waterLandings: number
    ) {
        this.serial = serial;
        this.status = status;
        this.type = type;
        this.reuseCount = reuseCount;
        this.waterLandings = waterLandings;
    }
}
