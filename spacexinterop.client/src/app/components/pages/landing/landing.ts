import { Component, OnInit } from '@angular/core';
import { SpaceXService } from '../../../shared/services/client/spacex.service';
import { SpaceXLaunchesRequest } from '../../../shared/classes/models/requests/SpaceXLaunchesRequest.model';
import { LaunchesRequestTypeEnum } from '../../../shared/enums/api/launch-request-type.enum';

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
      LaunchesRequestTypeEnum.Latest,
      1,
      10
    );

    this.spaceXService.getLaunches(request).then((result) => {
      if (result.isSuccess) {
        result.value?.items.forEach(element => {
          console.log(element.name);
        });
      } else {
        console.error(result.error);
      }
    });
  }
}
