import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LaunchResponse } from '../../../../shared/classes/models/responses/LaunchResponse.model';
import { EMPTY_STRING } from '../../../../shared/constants/common.constants';

@Component({
  selector: 'app-launch-details-dialog',
  imports: [],
  templateUrl: './launch-details-dialog.html',
  styleUrl: './launch-details-dialog.scss'
})
export class LaunchDetailsDialog {
  protected status: string = EMPTY_STRING;
  protected launchDate: string = EMPTY_STRING;

  constructor(@Inject(MAT_DIALOG_DATA) public data: LaunchResponse) {
    this.resolveData();
  }

  private resolveData(): void {
    this.status = this.data.success !== undefined 
      ? (this.data.success ? 'Success' : 'Failure') 
      : (this.data.upcoming ? 'Upcoming' : 'Unknown');

    const formattedDate = new Date(this.data.launchDateUtc).toLocaleDateString("en-US", {
      month: "numeric",
      day: "numeric",
      year: "numeric",
      hour: "2-digit",
      minute: "2-digit",
      second: "2-digit",
      timeZone: "UTC"
    });

    this.launchDate = formattedDate;
  }
}