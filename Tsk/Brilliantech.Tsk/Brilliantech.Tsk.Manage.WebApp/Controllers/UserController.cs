using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using Brilliantech.Tsk.Data.CL.Model;
using Brilliantech.Tsk.Data.CL.Repository.Implement;
using Brilliantech.Tsk.Data.CL.Repository.Interface;
using Brilliantech.Tsk.Manage.WebApp.Properties;
using Brilliantech.Tsk.Manage.WebApp.Util;

namespace Brilliantech.Tsk.Manage.WebApp.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        [Authorize]
        public ActionResult Index(int? page)
        {
            IPagedList<User> users = null;
            using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
            {
                int currentPageIndex = page.HasValue ? (page.Value <= 0 ? 0 : page.Value - 1) : 0;
                IUserRep userRep = new UserRep(unitOfWork);
                users = userRep.Queryable().ToPagedList(currentPageIndex, int.Parse(Resources.PageSize));
            }
            return View(users);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            User user;
            using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
            {
                IUserRep userRep = new UserRep(unitOfWork);
                user = userRep.FindById(id);
            }
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            User user = null;
            try
            {
                using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
                {
                    IUserRep userRep = new UserRep(unitOfWork);
                    user = userRep.FindById(id);
                    userRep.Delete(user);
                    unitOfWork.Submit();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(user);
            }
        }
    }
}
