using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Tsk.Data.CL.Model;

namespace Brilliantech.Tsk.Data.CL.Repository.Implement
{
    public class BaseRep
    {
        protected TskDataDataContext context;
        public BaseRep(IUnitOfWork _unit)
        {
            if (_unit == null)
            {
                throw new ArgumentNullException("Data context is null");
            }
            else {
                this.context = _unit as TskDataDataContext;
            }
        }
    }
}
