using Omini.Miq.Shared.Common;

namespace Omini.Miq.Business.Queries;

public class PaginationFilter
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string OrderBy { get; set; }
    public SortDirection Direction { get; set; }
}