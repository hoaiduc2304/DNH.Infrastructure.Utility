using System;
using System.ComponentModel.DataAnnotations;

namespace DNH.Infrastructure.Paging
{
    [Serializable]
    public class PagedResultRequestDto : LimitedResultRequestDto, IPagedResultRequest
    {
       
    }
}
