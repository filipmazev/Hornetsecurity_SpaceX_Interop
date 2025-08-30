import { Component, OnInit } from '@angular/core';
import { SpaceXService } from '../../../shared/services/client/spacex.service';
import { SpaceXLaunchesRequest } from '../../../shared/classes/models/requests/SpaceXLaunchesRequest.model';
import { SortDirectionEnum } from '../../../shared/enums/api/SortDirectionEnum';

@Component({
  selector: 'app-landing',
  imports: [],
  templateUrl: './landing.html',
  styleUrl: './landing.scss'
})
export class Landing implements OnInit {

  constructor(private spaceXService: SpaceXService) { }

  ngOnInit(): void {
    this.getLaunches();
  }

  private getLaunches(): void {
    const request = new SpaceXLaunchesRequest(
      false,
      SortDirectionEnum.Descending,
      3,
      10,
      true
    );

    this.spaceXService.getLaunches(request).then((result) => {
      if (result.isSuccess) {
        console.log(result.value);
      } else {
        console.error(result.error);
      }
    });
  }
}
