using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Tsk.Data.CL.Repository.Interface;
using Brilliantech.Tsk.Data.CL.Model;

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
    }
}
