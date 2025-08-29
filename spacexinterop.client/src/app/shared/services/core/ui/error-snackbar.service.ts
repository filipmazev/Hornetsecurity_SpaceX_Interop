import { inject, Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ERROR_SNACKBAR_DURATION_IN_SECONDS } from "../../../constants/animation.constants";

@Injectable({
    providedIn: 'root'
})
export class ErrorSnackbarService {
    private _snackBar = inject(MatSnackBar);

    constructor() { }

    displayError(errorMessage: string, durationInSeconds: number = ERROR_SNACKBAR_DURATION_IN_SECONDS) {
        this._snackBar.open(errorMessage, 'Close', {
            duration: durationInSeconds * 100000,
            verticalPosition: 'top',
            horizontalPosition: 'end'
        });
    }
}