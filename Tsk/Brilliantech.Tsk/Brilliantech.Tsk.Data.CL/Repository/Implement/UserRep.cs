using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Tsk.Data.CL.Repository.Interface;
using Brilliantech.Tsk.Data.CL.Model;

namespace Brilliantech.Tsk.Data.CL.Repository.Implement
{
    public class UserRep : BaseRep, IUserRep
    {
        public UserRep(IUnitOfWork context)
            : base(context)
        {

        }

        public void Create(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("user is null");
            }
            else
            {
                this.context.User.InsertOnSubmit(entity);
            }
        }
        public User Find(string name, string password = null)
        {
            var query = this.context.User.Where(u => u.Name.Equals(name));
            if (password != null)
            {
                query.Where(u => u.Password.Equals(password));
            }
            return query.FirstOrDefault();
        }
    }
}
