import { SortDirectionEnum } from "../../../enums/api/SortDirectionEnum";

export class SpaceXLaunchesRequest {
    searchText?: string;
    upcoming: boolean = false;
    sortDirection: SortDirectionEnum = SortDirectionEnum.Descending;
    pageIndex: number;
    pageSize: number;

    constructor(
        searchText: string | undefined,
        upcoming: boolean,
        sortDirection: SortDirectionEnum,
        pageIndex: number,
        pageSize: number
    ) {
        this.searchText = searchText;
        this.upcoming = upcoming;
        this.sortDirection = sortDirection;
        this.pageIndex = pageIndex;
        this.pageSize = pageSize;
    }
}