export class PaginatedResponse<TModel> {
    items: TModel[];
    totalCount: number;

    constructor(
        items: TModel[],
        totalCount: number,
    ) {
        this.items = items;
        this.totalCount = totalCount;
    }
}