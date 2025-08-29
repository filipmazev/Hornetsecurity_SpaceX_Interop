import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";
import { AuthService } from "../services/client/auth.service";

@Injectable({
    providedIn: 'root'
})
export class AuthGuard {
    constructor(
        private router: Router,
        private authService: AuthService
    ) { }

    async  canActivate(
        _next: ActivatedRouteSnapshot,
        _state: RouterStateSnapshot
    ): Promise<boolean> {
        debugger;
        if(_state.url === "/login" || _state.url === "/register") {
            return true;
        } else if (this.authService.isAuthenticated) {
            return true;
        } 

        this.router.navigate(['/login']);
        return false;
    }
}