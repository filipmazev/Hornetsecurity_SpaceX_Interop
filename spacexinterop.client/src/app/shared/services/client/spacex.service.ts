import { Injectable } from "@angular/core";
import { LaunchRowResponse } from "../../classes/models/responses/LaunchRowResponse.model";
import { PaginatedResponse } from "../../classes/models/responses/PaginatedResponse.model";
import { Result } from "../../classes/models/responses/Result.model";
import { HttpService } from "../core/http.service";
import { ErrorSnackbarService } from "../core/ui/error-snackbar.service";
import { environment } from "../../../../environments/environment";
import { COMMA_EMPTY_SPACE_JOINER } from "../../constants/common.constants";
import { SpaceXLaunchesRequest } from "../../classes/models/requests/SpaceXLaunchesRequest.model";
import { SpinnerService } from "../core/ui/spinner.service";
import { CompleteLaunchResponse } from "../../classes/models/responses/CompleteLaunchResponse.model";

@Injectable({
    providedIn: 'root'
})
export class SpaceXService {
    private baseUrl: string = 'SpaceX/';

    constructor(
        private httpService: HttpService,
        private spinnerService: SpinnerService,
        private errorSnackbarService: ErrorSnackbarService) {
    }

    public async getLaunchRows(request: SpaceXLaunchesRequest): Promise<Result<PaginatedResponse<LaunchRowResponse> | undefined | null>> {
        return new Promise<Result<PaginatedResponse<LaunchRowResponse> | undefined | null>>(async (resolve, reject) => {
            try {
                this.spinnerService.showSpinner();

                await this.httpService.post<Result<PaginatedResponse<LaunchRowResponse>>>(this.baseUrl + 'GetLaunchRows', request).then((result) => {
                    if (!result.isSuccess) {
                        this.errorSnackbarService.displayError(result.error?.messages.join(COMMA_EMPTY_SPACE_JOINER) ?? "An unknown error occurred while fetching launches.");
                    }
                    resolve(result);
                }).finally(() => {
                    this.spinnerService.hideSpinner();
                });
            } catch (error) {
                this.spinnerService.hideSpinner();
                this.errorSnackbarService.displayError("An unknown error occurred while fetching launches.");
                if (!environment.production) console.error('Fetch launches failed:', error);
                reject(error);
            }
        });
    }

    public async getCompleteLaunchById(id: string): Promise<Result<CompleteLaunchResponse | undefined | null>> {
        return new Promise<Result<CompleteLaunchResponse | undefined | null>>(async (resolve, reject) => {
            try {
                this.spinnerService.showSpinner();

                await this.httpService.get<Result<CompleteLaunchResponse | undefined | null>>(this.baseUrl + 'GetCompleteLaunchById/' + id).then((result) => {
                    if (!result.isSuccess) {
                        this.errorSnackbarService.displayError(result.error?.messages.join(COMMA_EMPTY_SPACE_JOINER) ?? "An unknown error occurred while fetching the launch with id: " + id);
                    }
                    resolve(result);
                }).finally(() => {
                    this.spinnerService.hideSpinner();
                });
            } catch (error) {
                this.spinnerService.hideSpinner();
                this.errorSnackbarService.displayError("An unknown error occurred while fetching the launch with id: " + id);
                if (!environment.production) console.error('Fetch launch by id failed:', error);
                reject(error);
            }
        });
    }
}