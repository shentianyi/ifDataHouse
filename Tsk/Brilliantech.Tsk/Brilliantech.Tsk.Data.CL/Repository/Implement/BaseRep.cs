using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Tsk.Data.CL.Model;

namespace Brilliantech.Tsk.Data.CL.Repository.Implement
{
    public class BaseRep
    {
        protected TskDataDataContext _context;
        public BaseRep(TskDataDataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Data context is null");
            }
            else {
                this._context = context;
            }
        }
    }
}
