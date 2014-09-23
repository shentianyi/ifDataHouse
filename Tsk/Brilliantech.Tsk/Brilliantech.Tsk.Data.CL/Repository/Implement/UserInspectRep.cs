using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Tsk.Data.CL.Repository.Interface;
using Brilliantech.Tsk.Data.CL.Model;

namespace Brilliantech.Tsk.Data.CL.Repository.Implement
{
    public class UserInspectRep : BaseRep, IUserInspectRep
    {
        public UserInspectRep(IUnitOfWork context)
            : base(context)
        {

        }



        public List<UserInspect> ListByUserId(int userId)
        {
            return context.UserInspect.Where(u => u.UserId.Equals(userId)).Take(100).ToList();
        }

        public void Create(UserInspect entity)
        {
            throw new NotImplementedException();
        }
    }
}
