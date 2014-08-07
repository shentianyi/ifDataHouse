using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliantech.Tsk.Data.CL.Model
{
    public partial class TskDataDataContext : IUnitOfWork
    {
        public void Submit()
        {
            this.SubmitChanges();
        }
    }
}
