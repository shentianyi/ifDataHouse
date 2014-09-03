using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace Brilliantech.Tsk.Manage.WebApp.Models
{
    public class InspectQueryModel
    {
        private string tskNo;
        private string leoniNo;
        private string cusNo;
        private string clipScanNo;
        private DateTime? clipScanTime1Start;
        private DateTime? clipScanTime1End;
        private DateTime? clipScanTime2Start;
        private DateTime? clipScanTime2End;
        private string tskScanNo;
        private DateTime? tskScanTime3Start;
        private DateTime? tskScanTime3End;
        private float? time3MinTime2Start;
        private float? time3MinTime2End;
        private string okOrNot;
        private DateTime? createdAtStart;
        private DateTime? createdAtEnd;
        private static List<string> csvHead = new List<string>() { 
         "TskNo","LeoniNo","CusNo","ClipScanNo","ClipScanTime1","ClipScanTime2","TskScanNo","TskScanTime3","Time3MinTime2","OkOrNot","数据上传时间"
        };
        private static List<string> fileds = new List<string>() { 
         "TskNo","LeoniNo","CusNo","ClipScanNo","ClipScanTime1View","ClipScanTime2View","TskScanNo","TskScanTime3View","Time3MinTime2View","OkOrNot","CreatedAtView"
        };


        public InspectQueryModel(NameValueCollection collection)
        {

            this.TskNo = collection.Get("TskNo");
            this.LeoniNo = collection.Get("LeoniNo");
            this.CusNo = collection.Get("CusNo");
            this.ClipScanNo = collection.Get("ClipScanNo");
            if (collection.Get("ClipScanTime1Start") != null && collection.Get("ClipScanTime1Start").Trim().Length > 0)
            {
                DateTime time = DateTime.Now;
                if (DateTime.TryParse(collection.Get("ClipScanTime1Start"), out time)) {
                    this.ClipScanTime1Start = time;
                }

               // this.ClipScanTime1Start = DateTime.Parse(collection.Get("ClipScanTime1Start"));
            }
            if (collection.Get("ClipScanTime1End") != null && collection.Get("ClipScanTime1End").Trim().Length > 0)
            {
                DateTime time = DateTime.Now;
                if (DateTime.TryParse(collection.Get("ClipScanTime1Start"), out time))
                {
                    this.ClipScanTime1End = time;
                }
               // this.ClipScanTime1End = DateTime.Parse(collection.Get("ClipScanTime1End"));
            }
            if (collection.Get("ClipScanTime2Start") != null && collection.Get("ClipScanTime2Start").Trim().Length > 0)
            {
                DateTime time = DateTime.Now;
                if (DateTime.TryParse(collection.Get("ClipScanTime2Start"), out time))
                {
                    this.ClipScanTime2Start = time;
                }
                //this.ClipScanTime2Start = DateTime.Parse(collection.Get("ClipScanTime2Start"));
            }
            if (collection.Get("ClipScanTime2End") != null && collection.Get("ClipScanTime2End").Trim().Trim().Length > 0)
            {
                DateTime time = DateTime.Now;
                if (DateTime.TryParse(collection.Get("ClipScanTime2End"), out time))
                {
                    this.ClipScanTime2End = time;
                }

                //this.ClipScanTime2End = DateTime.Parse(collection.Get("ClipScanTime2End"));
            }
            this.TskScanNo = collection.Get("TskScanNo");
            if (collection.Get("TskScanTime3Start") != null && collection.Get("TskScanTime3Start").Trim().Length > 0)
            {
                DateTime time = DateTime.Now;
                if (DateTime.TryParse(collection.Get("TskScanTime3Start"), out time))
                {
                    this.TskScanTime3Start = time;
                }

                //this.TskScanTime3Start = DateTime.Parse(collection.Get("TskScanTime3Start"));
            }
            if (collection.Get("TskScanTime3End") != null && collection.Get("TskScanTime3End").Trim().Length > 0)
            {
                DateTime time = DateTime.Now;
                if (DateTime.TryParse(collection.Get("TskScanTime3End"), out time))
                {
                    this.TskScanTime3End = time;
                }

               // this.TskScanTime3End = DateTime.Parse(collection.Get("TskScanTime3End"));
            }

            if (collection.Get("Time3MinTime2Start") != null && collection.Get("Time3MinTime2Start").Trim().Length > 0)
            {
                float i = 0;
                if (float.TryParse(collection.Get("Time3MinTime2Start"), out i))
                {
                    this.Time3MinTime2Start = i;
                }
                else
                {
                    this.time3MinTime2Start = null;
                }
            }
            if (collection.Get("Time3MinTime2End") != null && collection.Get("Time3MinTime2End").Trim().Length > 0)
            {
                float i = 0;
                if (float.TryParse(collection.Get("Time3MinTime2End"), out i))
                {
                    this.Time3MinTime2End = i;
                }
                else
                {
                    this.Time3MinTime2End = null;
                }
            }
            this.OkOrNot = collection.Get("OkOrNot");

            if (collection.Get("createdAtStart") != null && collection.Get("createdAtStart").Trim().Length > 0)
            {
                DateTime time = DateTime.Now;
                if (DateTime.TryParse(collection.Get("CreatedAtStart"), out time))
                {
                    this.CreatedAtStart = time;
                }
                //this.CreatedAtStart = DateTime.Parse(collection.Get("CreatedAtStart"));
            }
            if (collection.Get("CreatedAtEnd") != null && collection.Get("CreatedAtEnd").Trim().Length > 0)
            {
                DateTime time = DateTime.Now;
                if (DateTime.TryParse(collection.Get("CreatedAtEnd"), out time))
                {
                    this.CreatedAtEnd = time;
                }
               // this.CreatedAtEnd = DateTime.Parse(collection.Get("CreatedAtEnd"));
            }

        }
        public string TskNo
        {
            get { return tskNo; }
            set
            {
                tskNo = (value != null && value.Trim().Length > 0) ? value : null;
            }
        }
        public string LeoniNo
        {
            get { return leoniNo; }
            set
            {
                leoniNo = (value != null && value.Trim().Length > 0) ? value : null;
            }
        }
        public string CusNo
        {
            get { return cusNo; }
            set
            {
                cusNo = (value != null && value.Trim().Length > 0) ? value : null;
            }
        }
        public string ClipScanNo
        {
            get { return clipScanNo; }
            set
            {
                clipScanNo = (value != null && value.Trim().Length > 0) ? value : null;
            }
        }

        public DateTime? ClipScanTime1Start
        {
            get { return clipScanTime1Start; }
            set { clipScanTime1Start = value; }
        }

        public DateTime? ClipScanTime1End
        {
            get { return clipScanTime1End; }
            set { clipScanTime1End = value; }
        }

        public DateTime? ClipScanTime2Start
        {
            get { return clipScanTime2Start; }
            set { clipScanTime2Start = value; }
        }

        public DateTime? ClipScanTime2End
        {
            get { return clipScanTime2End; }
            set { clipScanTime2End = value; }
        }
        public string TskScanNo
        {
            get { return tskScanNo; }
            set
            {
                tskScanNo = (value != null && value.Trim().Length > 0) ? value : null;
            }
        }

        public DateTime? TskScanTime3Start
        {
            get { return tskScanTime3Start; }
            set { tskScanTime3Start = value; }
        }

        public DateTime? TskScanTime3End
        {
            get { return tskScanTime3End; }
            set { tskScanTime3End = value; }
        }

        public float? Time3MinTime2Start
        {
            get { return time3MinTime2Start; }
            set { time3MinTime2Start = value; }
        }

        public float? Time3MinTime2End
        {
            get { return time3MinTime2End; }
            set { time3MinTime2End = value; }
        }
        public string OkOrNot
        {
            get { return okOrNot; }
            set
            {
                okOrNot = (value != null && value.Trim().Length > 0) ? value : null;
            }
        }

        public DateTime? CreatedAtStart
        {
            get { return createdAtStart; }
            set { createdAtStart = value; }
        }


        public DateTime? CreatedAtEnd
        {
            get { return createdAtEnd; }
            set { createdAtEnd = value; }
        }
        public static List<string> CsvHead
        {
            get { return InspectQueryModel.csvHead; }
        }
        public static List<string> Fileds
        {
            get { return InspectQueryModel.fileds; }
        }

    }
}