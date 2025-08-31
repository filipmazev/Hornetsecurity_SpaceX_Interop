import { Component } from '@angular/core';
import { SpinnerService } from '../../../shared/services/core/ui/spinner.service';
import { MatProgressSpinner } from '@angular/material/progress-spinner';

@Component({
  selector: 'spinner',
  imports: [
    MatProgressSpinner
  ],
  templateUrl: './spinner.html',
  styleUrl: './spinner.scss'
})
export class Spinner {
  private _show_spinner: boolean = false;
  private set show_spinner(value: boolean) { this._show_spinner = value; }
  public get show_spinner(): boolean { return this._show_spinner; }

  constructor(private spinnerService: SpinnerService) { 
    this.CreateSubscriptions();
  }

  private CreateSubscriptions() {
    this.spinnerService.getShowSpinnerState$().subscribe((show_spinner) => {
      this.show_spinner = show_spinner;
    });
  }
}
