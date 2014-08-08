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
    }
}
