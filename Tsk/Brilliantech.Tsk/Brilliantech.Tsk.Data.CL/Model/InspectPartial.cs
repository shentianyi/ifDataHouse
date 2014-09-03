using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliantech.Tsk.Data.CL.Model
{
    partial class Inspect
    {
        public string ClipScanTime1View
        {
            get
            {
                return this.ClipScanTime1.HasValue ? this.ClipScanTime1.ToString() : "-";
            }
        }

        public string ClipScanTime2View
        {
            get
            {
                return this.ClipScanTime2.HasValue ? this.ClipScanTime2.ToString() : "-";
            }
        }


        public string TskScanTime3View
        {
            get
            {
                return this.TskScanTime3.HasValue ? this.TskScanTime3.ToString() : "-";
            }
        }

        public string Time3MinTime2View
        {
            get
            {
                return this.Time3MinTime2.HasValue ? this.Time3MinTime2.ToString() : "-";
            }

        }
    }
}
