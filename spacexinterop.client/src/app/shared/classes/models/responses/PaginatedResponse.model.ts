export class PaginatedResponse<TModel> {
    items: TModel[];
    totalItems: number;
    pageIndex: number;
    itemsPerPage: number;
    totalPages: number;

    constructor(
        items: TModel[],
        totalItems: number,
        pageIndex: number,
        itemsPerPage: number,
        totalPages: number
    ) {
        this.items = items;
        this.totalItems = totalItems;
        this.pageIndex = pageIndex;
        this.itemsPerPage = itemsPerPage;
        this.totalPages = totalPages;
    }
}