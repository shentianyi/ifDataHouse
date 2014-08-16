using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Tsk.Data.CL.Model;

namespace Brilliantech.Tsk.Data.CL.Repository.Interface
{
    public interface IUserRep:IBaseRep<User>
    {
        void Create(User entity);
        User Find(string name, string password = null);
    }
}
