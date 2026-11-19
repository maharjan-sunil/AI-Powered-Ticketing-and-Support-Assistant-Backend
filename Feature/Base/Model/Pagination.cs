namespace AI_Powered_Ticketing_and_Support_System_Backend.Feature.Base.Model
{
    public record PaginationParams
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 20;

        public int Page { get; init; } = 1;

        public int PageSize
        {
            get => _pageSize;
            init => _pageSize = value > MaxPageSize ? MaxPageSize : (value < 1 ? 1 : value);
        }
    }

    public record PagedResult<T>(
        IReadOnlyList<T> Items,
        int Page,
        int PageSize,
        int TotalCount)
    {
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasNext => Page < TotalPages;
        public bool HasPrevious => Page > 1;
    }
}
