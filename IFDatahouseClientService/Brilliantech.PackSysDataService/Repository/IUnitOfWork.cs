using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliantech.PackSysDataService.Repository
{
   public interface IUnitOfWork:IDisposable
    {
        void Submit();
    }
}
