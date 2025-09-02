import { SortDirectionEnum } from "../../../enums/api/SortDirectionEnum";

export class SpaceXLaunchesRequest {
    searchText?: string;
    upcoming: boolean = false;
    sortDirection: SortDirectionEnum = SortDirectionEnum.Descending;
    pageIndex: number;
    pageSize: number;
    includePayloads: boolean ;

    constructor(
        searchText: string | undefined,
        upcoming: boolean,
        sortDirection: SortDirectionEnum,
        pageIndex: number,
        pageSize: number,
        includePayloads: boolean
    ) {
        this.searchText = searchText;
        this.upcoming = upcoming;
        this.sortDirection = sortDirection;
        this.pageIndex = pageIndex;
        this.pageSize = pageSize;
        this.includePayloads = includePayloads;
    }
}