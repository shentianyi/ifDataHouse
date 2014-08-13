using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brilliantech.Tsk.Data.CL.Repository.Interface;
using Brilliantech.Tsk.Data.CL.Repository.Implement;
using Brilliantech.Tsk.Data.CL.Model;
using Brilliantech.Tsk.Manage.WebApp.Properties;
using Brilliantech.Tsk.Manage.WebApp.Util;
using MvcPaging;

namespace Brilliantech.Tsk.Manage.WebApp.Controllers
{
    public class InspectController : Controller
    {
        //
        // GET: /Inspect/
      //  IInspectRep inspectRep = new InspectRep();

        public ActionResult Index(int? page)
        {
            IPagedList<Inspect> inspects = null;
            using (IUnitOfWork unitOfWork = new TskDataDataContext())
            {
                int currentPageIndex = page.HasValue ? (page.Value<=0 ? 0 : page.Value - 1) : 0;
                IInspectRep inspectRep = new InspectRep(unitOfWork);
                inspects = inspectRep.Queryable().ToPagedList(currentPageIndex, int.Parse(Resources.PageSize), inspectRep.GetCount());
            }
            return View(inspects);
        }

        public ActionResult Query(FormCollection collection)
        {
             IPagedList<Inspect> inspects = null;
             using (IUnitOfWork unitOfWork = new TskDataDataContext())
             {
                 //int currentPageIndex = page.HasValue ? (page.Value <= 0 ? 0 : page.Value - 1) : 0;
                 //int pageSize = int.Parse(Resources.PageSize);
                 //IInspectRep inspectRep = new InspectRep(unitOfWork);
                 //  inspects = inspectRep.GetList(pageI, pageSize);
                 //int totalCount = inspectRep.GetCount();
                 //int totalPages = totalCount / pageSize + (totalCount % pageSize == 0 ? 0 : 1);
                 //pageList = new PageListUtil<Inspect>(inspects, pageI, pageSize, totalPages);
                 IInspectRep inspectRep = new InspectRep(unitOfWork);
             }
             return View("Index", inspects);
        }

        //
        // GET: /Inspect/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Inspect/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Inspect/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Inspect/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Inspect/Edit/5

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
        // GET: /Inspect/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Inspect/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
