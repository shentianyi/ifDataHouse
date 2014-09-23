using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Tsk.Data.CL.Repository.Interface;
using Brilliantech.Tsk.Data.CL.Model;

namespace Brilliantech.Tsk.Data.CL.Repository.Implement
{
    public class UserTskRep : BaseRep, IUserTskRep
    {
        public UserTskRep(IUnitOfWork context)
            : base(context)
        {

        }

        public void Create(UserTsk entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("user tsk is null");
            }
            else
            {
                this.context.UserTsk.InsertOnSubmit(entity);
            }
        }

        public void Delete(UserTsk entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("user tsk is null");
            }
            else
            {
                this.context.UserTsk.DeleteOnSubmit(entity);
            }
        }


        public UserTsk FindById(int id)
        {
            return this.context.UserTsk.FirstOrDefault(u => u.Id.Equals(id));
        }

        public List<UserTsk> ListByUserId(int userId)
        {
            return this.context.UserTsk.Where(u => u.UserId.Equals(userId)).ToList();
        }


        public UserTsk FindByUserId(int userId, string tskNo = null)
        {
            var query = this.context.UserTsk.Where(u => u.UserId.Equals(userId));
            if (tskNo != null)
            {
               query= query.Where(u => u.TskNo.Equals(tskNo));
            }
            List<UserTsk> userTsks= query.ToList();
            return userTsks.Count>0 ? userTsks.First() : null;
        }
    }
}
