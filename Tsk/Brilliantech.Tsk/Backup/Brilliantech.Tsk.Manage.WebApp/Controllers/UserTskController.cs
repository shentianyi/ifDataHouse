using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brilliantech.Tsk.Data.CL.Model;
using Brilliantech.Tsk.Manage.WebApp.Util;
using Brilliantech.Tsk.Data.CL.Repository.Interface;
using Brilliantech.Tsk.Data.CL.Repository.Implement;

namespace Brilliantech.Tsk.Manage.WebApp.Controllers
{
    public class UserTskController : Controller
    {
        //
        // GET: /UserTsk/
        [Authorize]
        public ActionResult Index(int userId)
        {
            List<UserTsk> userTsks = null;

            using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
            {
                Session["UserTsk_UserId"] = userId;
                IUserTskRep userTskRep = new UserTskRep(unitOfWork);
                userTsks = userTskRep.ListByUserId(userId);
            }
            return View(userTsks);
        }
         
        //
        // GET: /UserTsk/Create

        public ActionResult Create()
        {
          
            return View();
        } 

        //
        // POST: /UserTsk/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (Session["UserTsk_UserId"] == null)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    if (collection.Get("TskNo").Trim().Length > 0)
                    {
                        UserTsk userTsk;
                        using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
                        {
                            IUserTskRep userTskRep = new UserTskRep(unitOfWork);
                            UserTsk ut=userTskRep.FindByUserId((int)Session["UserTsk_UserId"], collection.Get("TskNo"));
                            if ( ut== null)
                            {
                                userTsk = new UserTsk() { UserId = (int)Session["UserTsk_UserId"], TskNo = collection.Get("TskNo") };
                                userTskRep.Create(userTsk);
                                unitOfWork.Submit();
                            }
                            else
                            {
                                TempData["Message"] = "此用户已配置TSK：" + collection.Get("TskNo");
                                return View();
                            }
                        }
                    }
                    else
                    {
                        TempData["Message"] = "TskNo长度需大于0";
                        return View();
                    }
                }
                return RedirectToAction("Index", new { userId = Session["UserTsk_UserId"] });
            }
            catch
            {
                return View();
            }
        }
        

        //
        // GET: /UserTsk/Delete/5

        public ActionResult Delete(int id)
        {
            UserTsk userTsk;
            using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
            {
                IUserTskRep userTskRep = new UserTskRep(unitOfWork);
                userTsk = userTskRep.FindById(id);
                if (userTsk != null)
                {
                    return View(userTsk);
                }
                else
                {
                    return RedirectToAction("Index", new { userId = Session["UserTsk_UserId"] });
                }
            }
        }

        //
        // POST: /UserTsk/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            UserTsk userTsk = null;
            try
            {
                using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
                {
                    IUserTskRep userTskRep = new UserTskRep(unitOfWork);
                    userTsk = userTskRep.FindById(id);
                    userTskRep.Delete(userTsk);
                    unitOfWork.Submit();
                }
                return RedirectToAction("Index", new { userId = Session["UserTsk_UserId"] });
            }
            catch
            {
                return View(userTsk);
            }
        }
    }
}
