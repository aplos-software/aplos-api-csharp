using System.Collections.Generic;

namespace AplosApi.Contract
{
    public class MetaModel
    {
        public int ResourceCount { get; set; }
        public Dictionary<string, string> AvailableFilters { get; set; }
    }

    public class LinksModel
    {
        public string Next { get; set; }
        public string Self { get; set; }
    }

    public class ApiResponse
    {
        public string Version { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public MetaModel Meta { get; set; }
        public LinksModel Links { get; set; }
    }

    public class PagedFilter
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}