using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Tsk.Data.CL.Model;

namespace Brilliantech.Tsk.Data.CL.Repository.Interface
{
    public interface IInspectRep : IBaseRep<Inspect>
    {
        int GetCount();
        IQueryable<Inspect> Queryable();
        IQueryable<Inspect> Queryable(DateTime? ClipScanTime1Start, DateTime? ClipScanTime1End);
        List<Inspect> GetList(int pageIndex = 0, int pageSize = 20); 
    }
}
