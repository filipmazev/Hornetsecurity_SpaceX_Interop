import { Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { firstValueFrom } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class HttpService {
    private apiPath: string = environment.apiPath;

    constructor(private httpClient: HttpClient) { }

    //#region HTTP Methods
    public async get<TModel>(endpoint: string, params?: Record<string, any>): Promise<TModel> {
        const url = this.apiPath + endpoint;
        return await firstValueFrom(this.httpClient.get<TModel>(url, { params, withCredentials: true }));
    }

    public async post<TModel>(endpoint: string, body: any = {}): Promise<TModel> {
        const url = this.apiPath + endpoint;

        try {
            return await firstValueFrom(this.httpClient.post<TModel>(url, body, { withCredentials: true }));
        } catch (error) {
            console.error(`POST ${url} failed:`, error);
            throw error;
        }
    }
    //#endregion
}   