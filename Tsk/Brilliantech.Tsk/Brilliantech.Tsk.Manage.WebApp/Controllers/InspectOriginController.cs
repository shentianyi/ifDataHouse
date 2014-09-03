using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using Brilliantech.Tsk.Data.CL.Model;
using Brilliantech.Tsk.Data.CL.Repository.Interface;
using Brilliantech.Tsk.Data.CL.Repository.Implement;
using Brilliantech.Tsk.Manage.WebApp.Properties;
using Brilliantech.Tsk.Manage.WebApp.Models;
using System.IO;
using System.Text;
using Brilliantech.Tsk.Manage.WebApp.Util;
using System.Transactions;

namespace Brilliantech.Tsk.Manage.WebApp.Controllers
{
    public class InspectOriginController : Controller
    {
        //
        // GET: /InspectOrigin/
        [Authorize]
        public ActionResult Index(int? page)
        {
            IPagedList<InspectOrigin> inspects = null;
            using (var txn = new System.Transactions.TransactionScope(TransactionScopeOption.Required,
               new TransactionOptions
               {
                   IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
               }))
            {
                using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
                {
                    int currentPageIndex = page.HasValue ? (page.Value <= 0 ? 0 : page.Value - 1) : 0;
                    IInspectOriginRep inspectOriginRep = new InspectOriginRep(unitOfWork);
                    inspects = inspectOriginRep.Queryable().ToPagedList(currentPageIndex, int.Parse(Resources.PageSize));
                }
            }

            return View(inspects);
        }
        [Authorize]
        public ActionResult Query()
        {
            InspectOriginQueryModel query = new InspectOriginQueryModel(Request.QueryString);
            int currentPageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out currentPageIndex);
            currentPageIndex = currentPageIndex <= 0 ? 0 : currentPageIndex - 1;
            int pageSize = int.Parse(Resources.PageSize);
            ViewBag.Query = query;
            return View("Index", QueryInspectOrigin(query, currentPageIndex, pageSize));
        }
        [Authorize]
        public void Export()
        {
            InspectOriginQueryModel query = new InspectOriginQueryModel(Request.QueryString);
            ViewBag.Query = query;
            List<InspectOrigin> inspects = ExportInspectOrigin(query);
            MemoryStream ms = new MemoryStream();
            using (StreamWriter sw = new StreamWriter(ms, Encoding.UTF8))
            {
                // write head
                //   string max =
                sw.WriteLine(string.Join(",", InspectOriginQueryModel.CsvHead.ToArray()));
                foreach (InspectOrigin i in inspects)
                {
                    List<string> ii = new List<string>();
                    foreach (string field in InspectOriginQueryModel.Fileds)
                    {
                      //  var p = i.GetType().GetProperties();
                       var value= i.GetType().GetProperty(field).GetValue(i, null);
                        ii.Add(value==null ? "" : value.ToString());
                    }
                    sw.WriteLine(string.Join(",", ii.ToArray()));
                }
                //sw.WriteLine(max);
            }
            var filename = "InspectOrigin" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
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

        public ActionResult Details(string id)
        {
            InspectOrigin inspect;
            using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
            {
                IInspectOriginRep inspectRep = new InspectOriginRep(unitOfWork);
                inspect = inspectRep.FindById(id);
            }
            return View(inspect);
        }
        private IPagedList<InspectOrigin> QueryInspectOrigin(InspectOriginQueryModel query, int? currentPageIndex, int? pageSize)
        {
            IPagedList<InspectOrigin> inspects;
            using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
            {
                IInspectOriginRep inspectRep = new InspectOriginRep(unitOfWork);
                inspects = GenerateQuery(unitOfWork, query).ToPagedList(currentPageIndex.Value, pageSize.Value);
            }
            return inspects;
        }

        private List<InspectOrigin> ExportInspectOrigin(InspectOriginQueryModel query)
        {
            List<InspectOrigin> inspects = new List<InspectOrigin>();
            using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
            {
                IInspectOriginRep inspectRep = new InspectOriginRep(unitOfWork);
                inspects = GenerateQuery(unitOfWork, query).ToList<InspectOrigin>();
            }
            return inspects;
        }
        private IQueryable<InspectOrigin> GenerateQuery(IUnitOfWork unitOfWork, InspectOriginQueryModel query)
        {
            IInspectOriginRep inspectRep = new InspectOriginRep(unitOfWork);
            return inspectRep.Queryable().Where(item => 
                //(string.IsNullOrEmpty(query.Text) ? true : item.Text.Contains(query.Text))&&
                 (query.ProcessResult.HasValue ? item.ProcessResult.Equals(query.ProcessResult) : true)
                && (query.CreatedAtStart.HasValue ? item.CreatedAt >= query.CreatedAtStart : true)
                && (query.CreatedAtEnd.HasValue ? item.CreatedAt <= query.CreatedAtEnd : true)
                );
        }
    }
}
