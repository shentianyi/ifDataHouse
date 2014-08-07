﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliantech.Tsk.Data.CL.Model
{
   public interface IUnitOfWork:IDisposable
    {
       void Submit();
    }
}
