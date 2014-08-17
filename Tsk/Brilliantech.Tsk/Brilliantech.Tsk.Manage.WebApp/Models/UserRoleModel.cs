using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brilliantech.Tsk.Manage.WebApp.Models
{
    public class UserRoleModel
    {
        private static Dictionary<string, UserRole> roles = new Dictionary<string, UserRole>() { 
        {"user",new UserRole(){Name="普通用户",Key="user"}},
        {"admin", new UserRole(){Name="管理员",Key="admin"}}
        };

        public static Dictionary<string, UserRole> Roles
        {
            get { return UserRoleModel.roles; }
            set { UserRoleModel.roles = value; }
        }

        public static List<UserRole> UserRoleList() {
            return roles.Values.ToList();
        }

        public static string RoleDisplay(string role)
        {
            return roles[role] == null ? "" : roles[role].Name;
        }
    }

    public class UserRole {
        public string Key { get; set; }
        public string Name { get; set; }
    }
}