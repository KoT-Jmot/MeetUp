namespace MeetUp.CommentsService.Application.RequestFeatures
{
    public class PagedList<T> : List<T>, IPagination
    {
        public MetaData? MetaData { get; set; }

        public PagedList(
            IEnumerable<T> items,
            int totalCount,
            int currentPage,
            int pageSize)
        {
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            MetaData = new MetaData(currentPage, totalPages, pageSize, totalCount);

            AddRange(items);
        }
        public static PagedList<T> ToPagedList(
            IEnumerable<T> source,
            int currentPage,
            int totalCount,
            int pageSize)
        {
            return new PagedList<T>(source, totalCount, currentPage, pageSize);
        }
    }
}
