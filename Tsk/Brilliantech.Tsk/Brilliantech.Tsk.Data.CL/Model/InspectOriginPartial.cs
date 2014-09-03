using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliantech.Tsk.Data.CL.Model
{
    public partial class InspectOrigin
    {
        public string CreatedAtView
        {
            get
            {
                return this.CreatedAt.Value.ToString("yyyy/M/d HH:mm:ss");
            }
        }
    }
}
