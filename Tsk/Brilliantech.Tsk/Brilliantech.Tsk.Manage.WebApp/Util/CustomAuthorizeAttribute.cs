using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Principal;
using Brilliantech.Tsk.Data.CL.Model;

namespace Brilliantech.Tsk.Manage.WebApp.Util
{
    public class CustomAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public string Role { get; set; }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            IPrincipal user = filterContext.HttpContext.User;
            if (!user.Identity.IsAuthenticated || !CustomMembershipProvider.IsAdmin(user.Identity.Name))
            {
                filterContext.Result =
                 new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary {{ "controller", "Account" },
                                             { "action", "LogOn" },
                                             { "returnUrl",    filterContext.HttpContext.Request.RawUrl } });
                return;
            }
        }
    }
}