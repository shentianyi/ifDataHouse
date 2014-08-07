using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliantech.Tsk.Data.CL.Repository.Interface
{
    public interface IBaseRep<T>
    {
        void Create(T entity);
    }
}
