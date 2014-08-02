using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Tsk.Data.CL.Model;

namespace Brilliantech.Tsk.Data.CL.Repository.Interface
{
    public interface IUserInspectRep : IBaseRep<UserInspect>
    {
        List<UserInspect> ListByUserId(int userId, DateTime? startTime = null, DateTime? endTime = null);
    }
}
