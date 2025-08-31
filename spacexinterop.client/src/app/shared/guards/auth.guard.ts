import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";
import { AuthService } from "../services/client/auth.service";

@Injectable({
    providedIn: 'root'
})
export class AuthGuard {
    private initialLoad: boolean = true;

    constructor(
        private authService: AuthService
    ) { }

    public canActivate(
        _next: ActivatedRouteSnapshot,
        _state: RouterStateSnapshot
    ): boolean {
        if(_state.url === "/login" || _state.url === "/register") {
            return true;
        } else if (!!this.authService.currentUser) {
            return true;
        }

        if(this.initialLoad) {
            this.initialLoad = false;
            return true;
        } else {
            return false;
        }
    }
}