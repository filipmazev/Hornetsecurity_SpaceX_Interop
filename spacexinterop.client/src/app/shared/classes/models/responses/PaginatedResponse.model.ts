export class PaginatedResponse<TModel> {
    items: TModel[];
    totalCount: number;
    pageIndex: number;
    itemsPerPage: number;
    totalPages: number;

    constructor(
        items: TModel[],
        totalCount: number,
        pageIndex: number,
        itemsPerPage: number,
        totalPages: number
    ) {
        this.items = items;
        this.totalCount = totalCount;
        this.pageIndex = pageIndex;
        this.itemsPerPage = itemsPerPage;
        this.totalPages = totalPages;
    }
}