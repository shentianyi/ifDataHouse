using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Quartz;
using Quartz.Impl;
using Brilliantech.Tsk.Manage.WebApp.Job;
using Brilliantech.Framwork.Utils.LogUtil;

namespace Brilliantech.Tsk.Manage.WebApp
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static IScheduler Scheduler;
      //  private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           
            routes.Add(new Route("{controller}.mvc/{action}",
                new MvcRouteHandler()) { Defaults = new RouteValueDictionary(new { controller = "Home" }) });

            routes.MapRoute(
               "Index",
                "{controller}/Index/{pageIndex}",
                new { action = "Index", pageIndex = UrlParameter.Optional }
               );


            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Inspect", action = "Index", id = UrlParameter.Optional } // 参数默认值
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            try
            {
                // job
                 
                ISchedulerFactory sf = new StdSchedulerFactory();
                Scheduler = sf.GetScheduler();

                new TskDataEmailCronTrigger().Run();
                Scheduler.Start();
                //TskDataEmailCronScheduler.Instance.Start();
            }
            catch (Exception e) {
                LogUtil.Logger.Error(e.Message);
            }
        }

        protected void Application_End()
        {
            LogUtil.Logger.Info("应用停止");
            Scheduler.Shutdown();
        }
    }
}