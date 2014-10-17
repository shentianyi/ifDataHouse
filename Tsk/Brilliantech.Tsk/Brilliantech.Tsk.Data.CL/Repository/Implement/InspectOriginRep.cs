using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Tsk.Data.CL.Repository.Interface;
using Brilliantech.Tsk.Data.CL.Model;
using System.Transactions;

namespace Brilliantech.Tsk.Data.CL.Repository.Implement
{
    public class InspectOriginRep : BaseRep, IInspectOriginRep
    {
        public InspectOriginRep(IUnitOfWork  context)
            : base(context)
        { 
        }
        /// <summary>
        /// 创建一条InspectOrigin数据
        /// </summary>
        /// <param name="entity">InspectOrigin 对象</param>
        public void Create(InspectOrigin entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("inspect origin is null");
            }
            else
            {
                this.context.InspectOrigin.InsertOnSubmit(entity);
            }
        }

        /// <summary>
        /// Get Count
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return context.InspectOrigin.Count();
        }

        public IQueryable<InspectOrigin> Queryable()
        {
            //using (var txn = new System.Transactions.TransactionScope(TransactionScopeOption.Required,
            //    new TransactionOptions
            //    {
            //        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
            //    }))
            //{
                return context.InspectOrigin.OrderByDescending(item => item.CreatedAt);
            //}
        }



        //public MvcPaging.IPagedList<InspectOrigin> GetPageList(int currentPageIndex, int pageSize)
        //{
        //    using (var txn = new System.Transactions.TransactionScope(TransactionScopeOption.Required,
        //     new TransactionOptions
        //     {
        //         IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
        //     }))
        //    {
        //        return context.InspectOrigin.Select().ToPagedList(currentPageIndex, pageSize);
        //    }
        //}
        /// <summary>
        /// Get List
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<InspectOrigin> GetList(int pageIndex = 0, int pageSize = 20)
        {
            return context.InspectOrigin.OrderByDescending(item => item.CreatedAt).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

       public InspectOrigin FindById(string id) {
           return context.InspectOrigin.FirstOrDefault(i => i.Id.Equals(id));
       }
       public IQueryable<InspectOrigin> Queryable(DateTime? CreatedAtStart, DateTime? CreatedAtEnd)
       {
           return context.InspectOrigin.Where(inspect => (CreatedAtStart.HasValue ? inspect.CreatedAt >= CreatedAtStart.Value : true)
               && (CreatedAtEnd.HasValue ? inspect.CreatedAt <= CreatedAtEnd.Value : true)).OrderByDescending(item => item.CreatedAt);
       }
    }
}
