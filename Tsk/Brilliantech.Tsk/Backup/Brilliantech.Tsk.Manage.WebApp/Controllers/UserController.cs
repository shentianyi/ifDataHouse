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
using Brilliantech.Tsk.Manage.WebApp.Models;

namespace Brilliantech.Tsk.Manage.WebApp.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        [Authorize]
        [CustomAuthorizeAttribute(Role = "admin")]
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
        [Authorize]
        [CustomAuthorizeAttribute(Role = "admin")]
        public ActionResult Edit(int id)
        {
            User user;
            using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
            {
                IUserRep userRep = new UserRep(unitOfWork);
                user = userRep.FindById(id);
                if (user != null)
                {
                    if (Brilliantech.Tsk.Manage.WebApp.Util.CustomMembershipProvider.CanEdit(user.Name))
                    {
                        ViewData["Role"] = new SelectList(UserRoleModel.UserRoleList(), "Key", "Name", user.Role);
                        return View(user);
                    }
                    else
                    {
                        TempData["Message"] = "初始管理员，不可以编辑";
                        return RedirectToAction("Index");
                    }
                }
                else {
                    return RedirectToAction("Index");
                }
            }
        }

        //
        // POST: /User/Edit/5
        [Authorize]
        [CustomAuthorizeAttribute(Role = "admin")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                User user;
                
                using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
                {
                    IUserRep userRep = new UserRep(unitOfWork);
                    user = userRep.FindById(id);

                    if (Brilliantech.Tsk.Manage.WebApp.Util.CustomMembershipProvider.CanEdit(user.Name))
                    {
                        ViewData["Role"] = new SelectList(UserRoleModel.UserRoleList(), "Key", "Name", user.Role);
                        if (collection.Get("Password").Trim().Length < CustomMembershipProvider.MinRequiredPasswordLength)
                        {
                            ViewBag.Message = "密码长度小于" + CustomMembershipProvider.MinRequiredPasswordLength;
                            return View(user);
                        }
                        else
                        {
                            user.Password = collection.Get("Password").Trim();
                            user.Role = collection.Get("Role");
                            user.Email = collection.Get("Email");
                            unitOfWork.Submit();
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "初始管理员，不可以编辑";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5
        [Authorize]
        [CustomAuthorizeAttribute(Role = "admin")]
        public ActionResult Delete(int id)
        {
            User user;
            using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
            {
                IUserRep userRep = new UserRep(unitOfWork);
                user = userRep.FindById(id); 
                if (user != null)
                {
                    if (Brilliantech.Tsk.Manage.WebApp.Util.CustomMembershipProvider.CanEdit(user.Name))
                    {
                        return View(user);
                    }
                    else
                    {
                        TempData["Message"] = "初始管理员，不可以删除";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        //
        // POST: /User/Delete/5
        [Authorize]
        [CustomAuthorizeAttribute(Role = "admin")]
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
