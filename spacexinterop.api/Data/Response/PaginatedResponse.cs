namespace spacexinterop.api.Data.Response;

public class PaginatedResponse<TAppItem>
    where TAppItem : class
{
    public required List<TAppItem> Items { get; set; } = [];
    public required int TotalItems { get; set; }
    public required int PageIndex { get; set; }
    public required int ItemsPerPage { get; set; }
    public required int TotalPages { get; set; }
}