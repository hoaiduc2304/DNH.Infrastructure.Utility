using System;
using System.Collections.Generic;
using System.Text;

namespace DNH.Infrastructure.Paging
{
    public interface IPagedResult<T> : IListResult<T>, IHasTotalCount
    {

    }
}
