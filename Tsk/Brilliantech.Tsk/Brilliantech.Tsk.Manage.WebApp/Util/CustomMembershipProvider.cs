using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Brilliantech.Tsk.Data.CL.Model;
using Brilliantech.Tsk.Data.CL.Repository.Interface;
using Brilliantech.Tsk.Data.CL.Repository.Implement;
using System.Web.Security;

namespace Brilliantech.Tsk.Manage.WebApp.Util
{
    public class CustomMembershipProvider
    {
        public int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public bool ValidateUser(string username, string password = null)
        {
            using (IUnitOfWork unitOfWork = new TskDataDataContext())
            {
                IUserRep userRep = new UserRep(unitOfWork);
                return userRep.Find(username, password) == null ? false : true;
            }
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            using (IUnitOfWork unitOfWork = new TskDataDataContext())
            {
                IUserRep userRep = new UserRep(unitOfWork);
                User user = userRep.Find(username, oldPassword);
                if (user != null)
                {
                    user.Password = newPassword;

                    unitOfWork.Submit();
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public void CreateUser(string username, string password, string role, out MembershipCreateStatus status)
        {
            status = MembershipCreateStatus.Success;
            using (IUnitOfWork unitOfWork = new TskDataDataContext())
            {
                IUserRep userRep = new UserRep(unitOfWork);

                if (userRep.Find(username) != null)
                {
                    status = MembershipCreateStatus.DuplicateUserName;
                }
                else
                {
                    userRep.Create(new User() { Name = username, Password = password ,Role=role});
                }
                unitOfWork.Submit();
            }
        }
    }
}