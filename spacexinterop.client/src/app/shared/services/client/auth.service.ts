import { Injectable } from "@angular/core";
import { HttpService } from "../core/http.service";
import { environment } from "../../../../environments/environment";
import { RegisterRequest } from "../../classes/models/requests/RegisterRequest";
import { Observable, Subject } from "rxjs";
import { LoginRequest } from "../../classes/models/requests/LoginRequest.model";
import { Result } from "../../classes/models/responses/Result.model";
import { CheckSessionResponse } from "../../classes/models/responses/CheckSessionResponse.model";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private baseUrl: string = 'Auth/';
    private isAuthenticatedSubject = new Subject<boolean>();
    
    private _isAuthenticated: boolean = false;
    public get isAuthenticated(): boolean { return this._isAuthenticated; }

    constructor(private httpService: HttpService) {
        this.createSubscriptions();
        this.whoAmI();
    }

    private createSubscriptions(){
        this.isAuthenticatedSubject.subscribe(value => this._isAuthenticated = value);
    }

    //#region Public Observables
    public getIsAuthenticated$(): Observable<boolean> {
        return this.isAuthenticatedSubject.asObservable();
    }
    //#endregion

    //#region Authentication
    public async login(data: LoginRequest): Promise<Result> {
        return new Promise<Result>(async (resolve, reject) => {
            try {
                await this.httpService.post<Result>(this.baseUrl + 'Login', data).then((result) => {
                    if(result.isSuccess) {
                        this.isAuthenticatedSubject.next(true);
                    } else {
                        // TODO: Handle failure
                    }
                    resolve(result);
                });
            } catch (error) {
                if (!environment.production) console.error('Login failed:', error);
                reject(error);
            }
        });
    }

    public async register(data: RegisterRequest): Promise<Result> {
        return new Promise<Result>(async (resolve, reject) => {
            try {
                await this.httpService.post<Result>(this.baseUrl + 'Register', data).then((result) => {
                    if(result.isSuccess) {
                        this.isAuthenticatedSubject.next(true);
                    } else {
                        // TODO: Handle failure
                    }
                    resolve(result);
                });
            } catch (error) {
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
                        // TODO: Handle failure
                    }
                    resolve(result);
                });
            } catch (error) {
                if (!environment.production) console.error('Login failed:', error);
                reject(error);
            }
        });
    }

    public async whoAmI(): Promise<CheckSessionResponse | undefined | null> {
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
            } catch {
                this.isAuthenticatedSubject.next(false);
                resolve(null);
            }
        });
    }
    //#endregion
}
