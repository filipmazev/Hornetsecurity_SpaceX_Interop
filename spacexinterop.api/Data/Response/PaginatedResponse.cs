namespace spacexinterop.api.Data.Response;

public class PaginatedResponse<TAppItem>
    where TAppItem : class
{
    public required List<TAppItem> Items { get; set; } = [];
    public required int TotalItems { get; set; }
}