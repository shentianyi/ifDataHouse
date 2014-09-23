using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Tsk.Data.CL.Model;

namespace Brilliantech.Tsk.Data.CL.Repository.Interface
{
    public interface IUserTskRep:IBaseRep<UserTsk>
    {
        void Create(UserTsk entity);
        UserTsk FindById(int id);
        UserTsk FindByUserId(int userId, string tskNo = null);
        void Delete(UserTsk entity);
        List<UserTsk> ListByUserId(int userId);
    }
}
