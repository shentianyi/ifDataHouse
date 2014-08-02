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



        public List<UserInspect> ListByUserId(int userId,DateTime? startTime=null,DateTime? endTime=null)
        {
            var query= context.UserInspect.Where(u => u.UserId.Equals(userId));
            if (startTime.HasValue && endTime.HasValue)
            {
                query = query.Where(u => (u.ClipScanTime1.Value >= startTime.Value && u.ClipScanTime1.Value <= endTime.Value));
            }
            return query.ToList();
        }

      

        public void Create(UserInspect entity)
        {
            throw new NotImplementedException();
        }
    }
}
