using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliantech.Tsk.Data.CL.Model
{
    public partial class User
    {
        public string LastLoginTimeView {
            get
            {
                return this.LastLoginTime.HasValue ?
                this.LastLoginTime.Value.ToString("yyyy/MM/dd HH:mm:ss") : "从未登录过";
            }
        }
    }
}
