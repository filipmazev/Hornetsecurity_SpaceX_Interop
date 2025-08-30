import { ResultStatusEnum } from "../../../enums/api/ResultStatusEnum";
import { Error } from "./Error.model";

export class Result<T = any> {
    status: ResultStatusEnum;
    isSuccess: boolean;
    error?: Error | null;
    value: T | null;

    constructor(
        status: ResultStatusEnum,
        isSuccess: boolean,
        error: Error | undefined | null,
        value: T | null
    ) {
        this.status = status;
        this.isSuccess = isSuccess;
        this.error = error;
        this.value = value;
    }
}
