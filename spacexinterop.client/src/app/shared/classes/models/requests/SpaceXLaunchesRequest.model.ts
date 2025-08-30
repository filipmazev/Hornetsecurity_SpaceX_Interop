import { LaunchesRequestTypeEnum } from "../../../enums/api/launch-request-type.enum";

export class SpaceXLaunchesRequest {
    launchesRequestType: LaunchesRequestTypeEnum;
    pageIndex: number;
    pageSize: number;

    constructor(
        launchesRequestType: LaunchesRequestTypeEnum,
        pageIndex: number, 
        pageSize: number) {
        this.launchesRequestType = launchesRequestType;
        this.pageIndex = pageIndex;
        this.pageSize = pageSize;
    }
}