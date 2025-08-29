import { Injectable } from "@angular/core";
import { HttpService } from "../core/http.service";
import { environment } from "../../../../environments/environment";
import { SignupRequest } from "../../classes/models/requests/SignupRequest.model";
import { Observable, Subject } from "rxjs";
import { LoginRequest } from "../../classes/models/requests/LoginRequest.model";
import { Result } from "../../classes/models/responses/Result.model";
import { WhoAmIResponse } from "../../classes/models/responses/WhoAmIResponse.model";

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
                await this.httpService.post<Result>(this.baseUrl + 'login', data).then((result) => {
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

    public async signup(data: SignupRequest): Promise<Result> {
        return new Promise<Result>(async (resolve, reject) => {
            try {
                await this.httpService.post<Result>(this.baseUrl + 'signup', data).then((result) => {
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
                await this.httpService.post<Result>(this.baseUrl + 'logout').then((result) => {
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

    public async whoAmI(): Promise<WhoAmIResponse | undefined | null> {
        return new Promise<WhoAmIResponse | undefined | null>(async (resolve) => {
            try {
                await this.httpService.get<Result<WhoAmIResponse>>(this.baseUrl + 'whoami').then((result) => {
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
