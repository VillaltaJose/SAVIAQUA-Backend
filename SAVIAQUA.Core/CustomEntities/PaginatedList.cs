namespace SAVIAQUA.Core.CustomEntities;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public bool HasPreviousPage => CurrentPage > 1;

    public bool HasNextPage => CurrentPage < TotalPages;

    public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : null;

    public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : null;

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        TotalPages = (int) Math.Ceiling(count / (double)pageSize);

        CurrentPage = pageNumber > TotalPages ? TotalPages : pageNumber;

        AddRange(items);
    }

    public static PagedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        var enumerable = source.ToList();
        var count = enumerable.Count();
        var items = enumerable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}