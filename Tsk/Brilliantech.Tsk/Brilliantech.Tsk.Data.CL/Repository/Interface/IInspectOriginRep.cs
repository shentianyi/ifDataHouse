using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Tsk.Data.CL.Model;

namespace Brilliantech.Tsk.Data.CL.Repository.Interface
{
    public interface IInspectOriginRep : IBaseRep<InspectOrigin>
    {
        int GetCount();
        IQueryable<InspectOrigin> Queryable();
        List<InspectOrigin> GetList(int pageIndex = 0, int pageSize = 20);
    }
}
