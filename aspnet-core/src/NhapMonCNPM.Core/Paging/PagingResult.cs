using System;
using System.Collections.Generic;
using System.Text;

namespace NhapMonCNPM.Paging
{
    public class PagingResult<T>
    {
        public long TotalItems { get; set; }
        public List<T> Items { get; set; }
    }
}
