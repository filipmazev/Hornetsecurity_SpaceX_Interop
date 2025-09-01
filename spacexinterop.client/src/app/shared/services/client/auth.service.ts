import { Injectable } from "@angular/core";
import { HttpService } from "../core/http.service";
import { environment } from "../../../../environments/environment";
import { RegisterRequest } from "../../classes/models/requests/RegisterRequest";
import { Observable, Subject } from "rxjs";
import { LoginRequest } from "../../classes/models/requests/LoginRequest.model";
import { Result } from "../../classes/models/responses/Result.model";
import { UserResponse } from "../../classes/models/responses/UserResponse.model";
import { ErrorSnackbarService } from "../core/ui/error-snackbar.service";
import { COMMA_EMPTY_SPACE_JOINER } from "../../constants/common.constants";
import { SpinnerService } from "../core/ui/spinner.service";
import { ResultStatusEnum } from "../../enums/api/ResultStatusEnum";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private baseUrl: string = 'Auth/';
    private currentUserSubject = new Subject<UserResponse | undefined>();

    private _currentUser: UserResponse | undefined = undefined;
    public get currentUser(): UserResponse | undefined { return this._currentUser; }

    constructor(
        private httpService: HttpService,
        private spinnerService: SpinnerService,
        private errorSnackbarService: ErrorSnackbarService
    ) {
        this.createSubscriptions();
        this.checkSession();
    }

    private createSubscriptions(){
        this.currentUserSubject.subscribe(value => this._currentUser = value);
    }

    //#region Public Observables
    public currentUser$(): Observable<UserResponse | undefined> {
        return this.currentUserSubject.asObservable();
    }
    //#endregion

    public async login(request: LoginRequest): Promise<Result<UserResponse | undefined>> {
        return new Promise<Result<UserResponse | undefined>>(async (resolve, reject) => {
            try {
                this.spinnerService.showSpinner();

                await this.httpService.post<Result<UserResponse | undefined>>(this.baseUrl + 'Login', request).then((result) => {
                    if(result.isSuccess) {
                        this.currentUserSubject.next(result.value ?? undefined);
                    } else if(result.status !== ResultStatusEnum.Unauthorized) {
                        this.errorSnackbarService.displayError(result.error?.messages.join(COMMA_EMPTY_SPACE_JOINER) ?? "An unknown error occurred during login.");
                    }
                    resolve(result);
                }).finally(() => {
                    this.spinnerService.hideSpinner();
                });
            } catch (error) {
                this.spinnerService.hideSpinner();
                this.errorSnackbarService.displayError("An unknown error occurred during login.");
                if (!environment.production) console.error('Login failed:', error);
                reject(error);
            }
        });
    }

    public async register(request: RegisterRequest): Promise<Result<UserResponse | undefined>> {
        return new Promise<Result<UserResponse | undefined>>(async (resolve, reject) => {
            try {
                this.spinnerService.showSpinner();

                await this.httpService.post<Result<UserResponse | undefined>>(this.baseUrl + 'Register', request).then((result) => {
                    if(result.isSuccess) {
                        this.currentUserSubject.next(result.value ?? undefined);
                    } else if(result.status !== ResultStatusEnum.EmailAlreadyExists) {
                        this.errorSnackbarService.displayError(result.error?.messages.join(COMMA_EMPTY_SPACE_JOINER) ?? "An unknown error occurred during registration.");
                    }
                    resolve(result);
                }).finally(() => {
                    this.spinnerService.hideSpinner();
                });
            } catch (error) {
                this.spinnerService.hideSpinner();
                this.errorSnackbarService.displayError("An unknown error occurred during registration.");
                if (!environment.production) console.error('Login failed:', error);
                reject(error);
            }
        });
    }

    public logout(): Promise<Result> {
        return new Promise<Result>(async (resolve, reject) => {
            try {
                this.spinnerService.showSpinner();

                await this.httpService.post<Result>(this.baseUrl + 'Logout').then((result) => {
                    if(result.isSuccess) {
                        this.currentUserSubject.next(undefined);
                    } else {
                        this.errorSnackbarService.displayError(result.error?.messages.join(COMMA_EMPTY_SPACE_JOINER) ?? "An unknown error occurred during logout.");
                    }
                    resolve(result);
                }).finally(() => {
                    this.spinnerService.hideSpinner();
                });
            } catch (error) {
                this.spinnerService.hideSpinner();
                this.errorSnackbarService.displayError("An unknown error occurred during logout.");
                if (!environment.production) console.error('Login failed:', error);
                reject(error);
            }
        });
    }

    public async checkSession(): Promise<UserResponse | undefined> {
        return new Promise<UserResponse | undefined>(async (resolve) => {
            try {
                await this.httpService.get<Result<UserResponse | undefined>>(this.baseUrl + 'CheckSession').then((result) => {
                    if(result.isSuccess && result.value) {
                        this.currentUserSubject.next(result.value);
                        resolve(result.value ?? undefined);
                        return;
                    } else if(result.status !== ResultStatusEnum.Unauthorized) {
                        this.errorSnackbarService.displayError(result.error?.messages.join(COMMA_EMPTY_SPACE_JOINER) ?? "An unknown error occurred during session check.");
                    }

                    this.currentUserSubject.next(undefined);
                    resolve(result.value ?? undefined);
                });
            } catch (error) {
                this.errorSnackbarService.displayError("An unknown error occurred during session check.");
                if (!environment.production) console.error('Session check failed:', error);
                resolve(undefined);
            }
        });
    }
}