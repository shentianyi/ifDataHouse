using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliantech.PackSysDataService.Repository
{
   public partial class PackSysDataClassesDataContext:IUnitOfWork
    {
        public void Submit()
       {
           this.SubmitChanges();
        } 
    }
}
