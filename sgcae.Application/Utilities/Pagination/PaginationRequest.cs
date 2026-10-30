namespace sgcae.Application.Utilities.Pagination
{
    public class PaginationRequest
    {
        public const int DefaultPageSize = 15;
        public const int MaxPageSize = 50;

        public int PageNumber { get; }
        public int PageSize { get; }

        public PaginationRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? DefaultPageSize : Math.Min(pageSize, MaxPageSize);
        }

        public static PaginationRequest Standard()
        {
            return new PaginationRequest(1, DefaultPageSize);
        }
    }
}
