using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Tsk.Data.CL.Model;

namespace Brilliantech.Tsk.Data.CL.Repository.Interface
{
    public interface IInspectRep:IBaseRep<Inspect>
    {
        int GetCount();
        IQueryable<Inspect> Queryable();
        List<Inspect> GetList(int pageIndex = 0, int pageSize = 20);
        List<Inspect> Query(Dictionary<string, string> query);
    }
}
