import { Injectable } from "@angular/core";
import { HttpService } from "../core/http.service";
import { environment } from "../../../../environments/environment";
import { RegisterRequest } from "../../classes/models/requests/RegisterRequest";
import { Observable, Subject } from "rxjs";
import { LoginRequest } from "../../classes/models/requests/LoginRequest.model";
import { Result } from "../../classes/models/responses/Result.model";
import { CheckSessionResponse } from "../../classes/models/responses/CheckSessionResponse.model";
import { ErrorSnackbarService } from "../core/ui/error-snackbar.service";
import { COMMA_EMPTY_SPACE_JOINER } from "../../constants/common.constants";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private baseUrl: string = 'Auth/';
    private isAuthenticatedSubject = new Subject<boolean>();
    
    private _isAuthenticated: boolean = false;
    public get isAuthenticated(): boolean { return this._isAuthenticated; }

    constructor(
        private httpService: HttpService,
        private errorSnackbarService: ErrorSnackbarService
    ) {
        this.createSubscriptions();
        this.checkSession();
    }

    private createSubscriptions(){
        this.isAuthenticatedSubject.subscribe(value => this._isAuthenticated = value);
    }

    //#region Public Observables
    public getIsAuthenticated$(): Observable<boolean> {
        return this.isAuthenticatedSubject.asObservable();
    }
    //#endregion

    public async login(request: LoginRequest): Promise<Result> {
        return new Promise<Result>(async (resolve, reject) => {
            try {
                await this.httpService.post<Result>(this.baseUrl + 'Login', request).then((result) => {
                    if(result.isSuccess) {
                        this.isAuthenticatedSubject.next(true);
                    }
                    resolve(result);
                });
            } catch (error) {
                this.errorSnackbarService.displayError("An unknown error occurred during login.");
                if (!environment.production) console.error('Login failed:', error);
                reject(error);
            }
        });
    }

    public async register(request: RegisterRequest): Promise<Result> {
        return new Promise<Result>(async (resolve, reject) => {
            try {
                await this.httpService.post<Result>(this.baseUrl + 'Register', request).then((result) => {
                    if(result.isSuccess) {
                        this.isAuthenticatedSubject.next(true);
                    }
                    resolve(result);
                });
            } catch (error) {
                this.errorSnackbarService.displayError("An unknown error occurred during registration.");
                if (!environment.production) console.error('Login failed:', error);
                reject(error);
            }
        });
    }

    public logout(): Promise<Result> {
        return new Promise<Result>(async (resolve, reject) => {
            try {
                await this.httpService.post<Result>(this.baseUrl + 'Logout').then((result) => {
                    if(result.isSuccess) {
                        this.isAuthenticatedSubject.next(false);
                    } else {
                        this.errorSnackbarService.displayError(result.error?.messages.join(COMMA_EMPTY_SPACE_JOINER) ?? "An unknown error occurred during logout.");
                    }
                    resolve(result);
                });
            } catch (error) {
                this.errorSnackbarService.displayError("An unknown error occurred during logout.");
                if (!environment.production) console.error('Login failed:', error);
                reject(error);
            }
        });
    }

    public async checkSession(): Promise<CheckSessionResponse | undefined | null> {
        return new Promise<CheckSessionResponse | undefined | null>(async (resolve) => {
            try {
                await this.httpService.get<Result<CheckSessionResponse>>(this.baseUrl + 'CheckSession').then((result) => {
                    if(result.isSuccess && result.value) {
                        this.isAuthenticatedSubject.next(true);
                    } else {
                        this.isAuthenticatedSubject.next(false);
                    }
                    resolve(result.value);
                });
            } catch (error) {
                this.errorSnackbarService.displayError("An unknown error occurred during session check.");
                if (!environment.production) console.error('Session check failed:', error);
                resolve(null);
            }
        });
    }
}