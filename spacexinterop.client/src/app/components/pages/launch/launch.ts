import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ID_NAVIGATION_PARAM } from '../../../shared/constants/navigatory.constants';
import { SpaceXService } from '../../../shared/services/client/spacex.service';
import { CompleteLaunchResponse } from '../../../shared/classes/models/responses/CompleteLaunchResponse.model';
import { EMPTY_STRING } from '../../../shared/constants/common.constants';
import { NgClass } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { MatIcons } from '../../../shared/enums/ui/mat-icons.enum';

@Component({
  selector: 'app-launch',
  imports: [
    NgClass,
    MatIcon
  ],
  templateUrl: './launch.html',
  styleUrls: ['./launch.scss', '../launches/launches.scss']
})
export class Launch {
  private id: string | null = null;
  protected data?: CompleteLaunchResponse;

  protected status: string = EMPTY_STRING;
  protected launchDate: string = EMPTY_STRING;

  protected allShips: string = EMPTY_STRING;

  protected MatIcons = MatIcons;

  constructor(
    private spaceXService: SpaceXService,
    private route: ActivatedRoute) {
    this.id = this.route.snapshot.paramMap.get(ID_NAVIGATION_PARAM);

    this.route.paramMap.subscribe(params => {
      this.id = params.get(ID_NAVIGATION_PARAM);
      this.fetchData();
    });
  }

  private fetchData(): void {
    if (!this.id) return;

    this.spaceXService.getCompleteLaunchById(this.id).then(result => {
      if (result.isSuccess && result.value) {
        this.data = result.value;
        this.resolveData(this.data);
      }
    });
  }

  private resolveData(data: CompleteLaunchResponse): void {
    this.status = data.success !== undefined
      ? (data.success ? 'Success' : 'Failure')
      : (data.upcoming ? 'Upcoming' : 'Unknown');

    const formattedDate = new Date(data.launchDateUtc).toLocaleDateString("en-US", {
      month: "numeric",
      day: "numeric",
      year: "numeric",
      hour: "2-digit",
      minute: "2-digit",
      second: "2-digit",
      timeZone: "UTC"
    });

    this.launchDate = formattedDate;

    this.allShips = data.ships.map(ship => ship.name).join(', ');
  }
}
