namespace Sibres.Business;

public class PaginatedList<T>
{
    public int Page { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;

    public List<T> Data { get; set; } = new();

    public PaginatedList(IEnumerable<T> items, int count, int page, PageSize pageSize)
    {
        Page = page;
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = (int)pageSize;

        Data.AddRange(items);
    }

    public static PaginatedList<T> Create(IEnumerable<T> source, int page, PageSize pageSize)
    {
        var count = source.Count();
        var _pageSize = (int)pageSize;

        var result = page == 1 ? source.Skip(0).Take(_pageSize).ToList() : 
            source.Skip((page - 1) * _pageSize).Take<T>(_pageSize).ToList();

        return new PaginatedList<T>(result, count, page, pageSize);
    }
}