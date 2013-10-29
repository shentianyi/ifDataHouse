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

        public List<PackItemView> GetByTime(DateTime startTime, DateTime endTime)
        {
            return context.PackItemView.Where(p => p.packagingTime >= startTime && p.packagingTime < endTime).ToList();
        }
    }
}
