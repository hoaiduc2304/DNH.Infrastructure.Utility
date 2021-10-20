using System;
using System.Collections.Generic;
using System.Text;

namespace DNH.Infrastructure.Paging
{
    public class PagingRequest
    {
        public PagedResultRequestDto SearchInfo { get; set; }
        public string TableName { get; set; }
    }

}
