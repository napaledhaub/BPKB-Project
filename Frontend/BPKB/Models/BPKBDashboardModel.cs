﻿namespace BPKB.Models
{
    public class BPKBDashboardModel
    {
        public List<BPKBModel> BPKBs { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
