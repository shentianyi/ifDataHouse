using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brilliantech.Tsk.Manage.WebApp.Models
{
    public class UserRoleModel
    {
        private static Dictionary<string, string> roles = new Dictionary<string, string>() { 
        {"user","普通用户"},
        {"admin","管理员"}
        };

        public static Dictionary<string, string> Roles
        {
            get { return UserRoleModel.roles; }
            set { UserRoleModel.roles = value; }
        }
    }
}