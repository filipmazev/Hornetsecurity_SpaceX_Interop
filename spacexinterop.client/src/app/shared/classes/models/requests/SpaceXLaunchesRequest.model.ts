import { SortDirectionEnum } from "../../../enums/api/SortDirectionEnum";

export class SpaceXLaunchesRequest {
    upcoming: boolean = false;
    sortDirection: SortDirectionEnum = SortDirectionEnum.Descending;
    pageIndex: number;
    pageSize: number;
    includePayloads: boolean ;

    constructor(
        upcoming: boolean,
        sortDirection: SortDirectionEnum,
        pageIndex: number,
        pageSize: number,
        includePayloads: boolean
    ) {
        this.upcoming = upcoming;
        this.sortDirection = sortDirection;
        this.pageIndex = pageIndex;
        this.pageSize = pageSize;
        this.includePayloads = includePayloads;
    }
}