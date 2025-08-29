import { Error } from "../models/responses/Error.model";
import { Result } from "../models/responses/Result.model";

export class ResultExtensions {
    static match<T>(result: Result<T>, onSuccess: () => T, onFailure: (error?: Error | null) => T): T {
        return result.isSuccess ? onSuccess() : onFailure(result.error);
    }

    static matchVoid(result: Result, onSuccess: () => void, onFailure: (error?: Error | null) => void): void {
        if (result.isSuccess) {
            onSuccess();
        } else {
            onFailure(result.error);
        }
    }
}
