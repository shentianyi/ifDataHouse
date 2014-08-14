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
using Brilliantech.Tsk.Manage.WebApp.Models;
using System.IO;
using System.Text;

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
                inspects = inspectRep.Queryable().ToPagedList(currentPageIndex, int.Parse(Resources.PageSize));
            }
            return View(inspects);
        }

        public ActionResult Query()
        {
            InspectQueryModel query = new InspectQueryModel(Request.QueryString);
            int currentPageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out currentPageIndex);
            currentPageIndex = currentPageIndex <= 0 ? 0 : currentPageIndex - 1;
            int pageSize = int.Parse(Resources.PageSize);
            ViewBag.Query = query; 
            return View("Index", QueryInspect(query, currentPageIndex, pageSize));
        }

        public void Export()
        {
            InspectQueryModel query = new InspectQueryModel(Request.QueryString);
            ViewBag.Query = query;
            List<Inspect> inspects = ExportInspect(query);
            MemoryStream ms = new MemoryStream();
            using (StreamWriter sw = new StreamWriter(ms, Encoding.UTF8))
            {
                // write head
               //   string max =
                 sw.WriteLine(string.Join(";",InspectQueryModel.CsvHead.ToArray()));
                foreach(Inspect i in inspects){
                    List<string> ii = new List<string>(); 
                    foreach (string field in InspectQueryModel.Fileds) {
                        ii.Add(i.GetType().GetProperty(field).GetValue(i,null).ToString());
                    }
                    sw.WriteLine(string.Join(";", ii.ToArray()));
                }
                //sw.WriteLine(max);
            } 
            var filename = "Inspect"+DateTime.Now.ToString("yyyyMMddHHmmss")+".csv";
            var contenttype = "text/csv";
            Response.Clear();
            Response.ContentEncoding = Encoding.UTF8;
            Response.ContentType = contenttype;
            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(ms.ToArray());
            Response.End();
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

        private IPagedList<Inspect> QueryInspect(InspectQueryModel query, int? currentPageIndex,int? pageSize)
        {
            IPagedList<Inspect> inspects; 
            using (IUnitOfWork unitOfWork = new TskDataDataContext())
            {
                IInspectRep inspectRep = new InspectRep(unitOfWork);
                inspects = GenerateQuery(unitOfWork,query).ToPagedList(currentPageIndex.Value, pageSize.Value);
            }
            return inspects;
        }

        private List<Inspect> ExportInspect(InspectQueryModel query)
        {
            List<Inspect> inspects = new List<Inspect>();
            using (IUnitOfWork unitOfWork = new TskDataDataContext())
            {
                IInspectRep inspectRep = new InspectRep(unitOfWork);
                inspects = GenerateQuery(unitOfWork, query).ToList<Inspect>();
            }
            return inspects;
        }
        private IQueryable<Inspect> GenerateQuery(IUnitOfWork unitOfWork, InspectQueryModel query)
        {
            IInspectRep inspectRep = new InspectRep(unitOfWork);
            return inspectRep.Queryable().Where(item => (string.IsNullOrEmpty(query.TskNo) ? true : item.TskNo.Contains(query.TskNo))
                && (string.IsNullOrEmpty(query.LeoniNo) ? true : item.LeoniNo.Contains(query.LeoniNo))
                && (string.IsNullOrEmpty(query.CusNo) ? true : item.CusNo.Contains(query.CusNo))
                && (string.IsNullOrEmpty(query.ClipScanNo) ? true : item.ClipScanNo.Contains(query.ClipScanNo))
                && (query.ClipScanTime1Start.HasValue ? item.ClipScanTime1 >= query.ClipScanTime1Start : true)
                && (query.ClipScanTime1End.HasValue ? item.ClipScanTime1 <= query.ClipScanTime1End : true)
                && (query.ClipScanTime2Start.HasValue ? item.ClipScanTime2 >= query.ClipScanTime2Start : true)
                && (query.ClipScanTime2End.HasValue ? item.ClipScanTime2 <= query.ClipScanTime2End : true)
                && (string.IsNullOrEmpty(query.TskScanNo) ? true : item.TskScanNo.Contains(query.TskScanNo))
                && (query.TskScanTime3Start.HasValue ? item.TskScanTime3 >= query.TskScanTime3Start : true)
                && (query.TskScanTime3End.HasValue ? item.TskScanTime3 <= query.TskScanTime3End : true)
                && (query.Time3MinTime2Start.HasValue ? item.Time3MinTime2 >= query.Time3MinTime2Start : true)
                && (query.Time3MinTime2End.HasValue ? item.Time3MinTime2 <= query.Time3MinTime2End : true)
                && (query.CreatedAtStart.HasValue ? item.CreatedAt >= query.CreatedAtStart : true)
                && (query.CreatedAtEnd.HasValue ? item.CreatedAt <= query.CreatedAtEnd : true)
                && (string.IsNullOrEmpty(query.OkOrNot) ? true : item.OkOrNot.Contains(query.OkOrNot))
                );
        }
    }
}
