namespace AppPrimiani.Core.Responses
{
    public class PagedResponse<TData> : Response<TData>
    {
        [System.Text.Json.Serialization.JsonConstructor]
        public PagedResponse(TData? data, int totalCount, int currentPage = 1, int pageSize = Configuration.DefaultPageSize)
            :base(data)
        {
            Data = data;
            TotalCount = totalCount;
            PageSize = Configuration.DefaultPageSize;
            CurrentPage = 1;
        }

        public PagedResponse(TData? data, int code = Configuration.DefaultStatusCode, string? message = null )
            : base(data, code, message)
        {
            //Data = data;
            //Message = message;
            //_code = code;
        }
        public int CurrentPage { get; set; }
        public int TotalPages 
            => (int)Math.Ceiling(TotalCount/(double)PageSize);
        public int PageSize { get; set; } = Configuration.DefaultPageSize;
        public int TotalCount { get; set; }
    }
}
