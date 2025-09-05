import { EMPTY_STRING } from "../../../constants/common.constants";
import { CoreStatusEnum } from "./CoreStatusEnum";


export class CoreResponse {
    serial: string = EMPTY_STRING;
    block?: number;
    status!: CoreStatusEnum;
    reuseCount: number = 0;
    lastUpdate?: string;

    constructor(
        serial: string,
        block: number | undefined,
        status: CoreStatusEnum,
        reuseCount: number,
        lastUpdate: string | undefined
    ) {
        this.serial = serial;
        this.block = block;
        this.status = status;
        this.reuseCount = reuseCount;
        this.lastUpdate = lastUpdate;
    }
}
