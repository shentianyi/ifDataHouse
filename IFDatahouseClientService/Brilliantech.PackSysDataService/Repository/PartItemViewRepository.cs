using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliantech.PackSysDataService.Repository
{
    public class PackItemViewRepository
    {
        PackSysDataClassesDataContext context;

        public PackItemViewRepository(IUnitOfWork _context)
        {
            this.context = _context as PackSysDataClassesDataContext;
        }

        public List<PackItemView> GetByTime(string startTime, string endTime)
        {
            return context.ExecuteQuery<PackItemView>("SELECT * FROM PackItemView where packagingTime>={0} and packagingTime<{1}", startTime, endTime).ToList();
            //return context.PackItemView.Where(p => p.packagingTime >= startTime && p.packagingTime < endTime).ToList();
        }
    }
}
