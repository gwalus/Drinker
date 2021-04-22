namespace DrinkerAPI.Helpers
{
    public class PaginationParams
    {
        private const int _maxPageSize = 24;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 8;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
        }
    }
}
