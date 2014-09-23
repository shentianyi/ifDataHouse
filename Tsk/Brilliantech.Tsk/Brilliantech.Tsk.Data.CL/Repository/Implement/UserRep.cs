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
               query= query.Where(u => u.Password.Equals(password));
            }

            List<User> users = query.ToList();
            return users.Count > 0 ? users.First() : null;
        }

        public User FindById(int id) {
            return this.context.User.FirstOrDefault(u => u.Id.Equals(id));
        }

        public IQueryable<User> Queryable()
        {
            return context.User;
        }


        public void Delete(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("user is null");
            }
            else
            {
                this.context.User.DeleteOnSubmit(entity);
            }
        }

        public List<User> ListWithEmail()
        {
            return this.context.User.Where(u => ((!u.Email.Equals(null)) && u.Email.Trim().Length>0)).ToList();
        }
    }
}
