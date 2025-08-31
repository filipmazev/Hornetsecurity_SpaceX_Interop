import { Injectable } from "@angular/core";
import { LaunchResponse } from "../../classes/models/responses/LaunchResponse.model";
import { PaginatedResponse } from "../../classes/models/responses/PaginatedResponse.model";
import { Result } from "../../classes/models/responses/Result.model";
import { HttpService } from "../core/http.service";
import { ErrorSnackbarService } from "../core/ui/error-snackbar.service";
import { environment } from "../../../../environments/environment";
import { COMMA_EMPTY_SPACE_JOINER } from "../../constants/common.constants";
import { SpaceXLaunchesRequest } from "../../classes/models/requests/SpaceXLaunchesRequest.model";

@Injectable({
    providedIn: 'root'
})
export class SpaceXService {
    private baseUrl: string = 'SpaceX/';

    constructor(
        private httpService: HttpService,
        private errorSnackbarService: ErrorSnackbarService) {
    }

    public async getLaunches(request: SpaceXLaunchesRequest): Promise<Result<PaginatedResponse<LaunchResponse>>> {
        return new Promise<Result<PaginatedResponse<LaunchResponse>>>(async (resolve, reject) => {
            try {
                await this.httpService.post<Result<PaginatedResponse<LaunchResponse>>>(this.baseUrl + 'GetLaunches', request).then((result) => {
                    if (!result.isSuccess) {
                        this.errorSnackbarService.displayError(result.error?.messages.join(COMMA_EMPTY_SPACE_JOINER) ?? "An unknown error occurred while fetching launches.");
                    }
                    resolve(result);
                });
            } catch (error) {
                this.errorSnackbarService.displayError("An unknown error occurred while fetching launches.");
                if (!environment.production) console.error('Fetch launches failed:', error);
                reject(error);
            }
        });
    }
}