using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Tsk.Data.CL.Repository.Interface;
using Brilliantech.Tsk.Data.CL.Model;

namespace Brilliantech.Tsk.Data.CL.Repository.Implement
{
    public class InspectRep : BaseRep, IInspectRep
    {
        public InspectRep(IUnitOfWork context)
            : base(context)
        {

        }
        /// <summary>
        /// 创建一条Inspect数据
        /// </summary>
        /// <param name="entity">Inspect 对象</param>
        public void Create(Inspect entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("inspect is null");
            }
            else
            {
                this.context.Inspect.InsertOnSubmit(entity);
            }
        }

        /// <summary>
        /// Get Count
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        { 
            return context.Inspect.Count();
        }

        public IQueryable<Inspect> Queryable() {
            return context.Inspect.OrderByDescending(item => item.CreatedAt);
        }

        /// <summary>
        /// Get List
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Inspect> GetList(int pageIndex = 0, int pageSize = 20)
        {
            return context.Inspect.OrderByDescending(item=>item.CreatedAt).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }



        public IQueryable<Inspect> Queryable(DateTime? ClipScanTime1Start, DateTime? ClipScanTime1End)
        {
            return context.Inspect.Where(inspect => (ClipScanTime1Start.HasValue ? inspect.ClipScanTime1 >= ClipScanTime1Start.Value : true)
                && (ClipScanTime1End.HasValue ? inspect.ClipScanTime1<=ClipScanTime1End.Value : true)).OrderByDescending(item => item.CreatedAt);
        }
    }
}
