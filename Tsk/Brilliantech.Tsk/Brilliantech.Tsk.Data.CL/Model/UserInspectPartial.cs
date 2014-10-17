using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliantech.Tsk.Data.CL.Model
{
    partial class UserInspect
    {
        public string ClipScanTime1View
        {
            get
            {
                return this.ClipScanTime1.HasValue ? this.ClipScanTime1.Value.ToString("yyyy/M/d HH:mm:ss") : "-";
            }
        }

        public string ClipScanTime2View
        {
            get
            {
                return this.ClipScanTime2.HasValue ? this.ClipScanTime2.Value.ToString("yyyy/M/d HH:mm:ss") : "-";
            }
        }


        public string TskScanTime3View
        {
            get
            {
                return this.TskScanTime3.HasValue ? this.TskScanTime3.Value.ToString("yyyy/M/d HH:mm:ss") : "-";
            }
        }

        public string Time3MinTime2View
        {
            get
            {
                return this.Time3MinTime2.HasValue ? this.Time3MinTime2.Value.ToString() : "-";
            }

        }

        public string CreatedAtView
        {
            get
            {
                return this.CreatedAt.Value.ToString("yyyy/M/d HH:mm:ss");
            }
        }
    }
}
