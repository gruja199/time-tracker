﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackerAPI.Client.Models
{
    public class PageList<T>
    {

        public IEnumerable<T> Items { get; set; }
        public int Page { get; set; }

        public int PageSize { get; set;  }

        public int TotalCount { get; set; }

        public int TotalPages => 
            (int) Math.Ceiling(TotalCount/ (decimal)PageSize) ;

    }
}
