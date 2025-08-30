import { Error } from "./Error.model";
import { ResultStatus } from "../../../enums/api/result-status.enum";

export class Result<T = any> {
    status: ResultStatus;
    isSuccess: boolean;
    error?: Error | null;
    value: T | null;

    constructor(
        status: ResultStatus,
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
