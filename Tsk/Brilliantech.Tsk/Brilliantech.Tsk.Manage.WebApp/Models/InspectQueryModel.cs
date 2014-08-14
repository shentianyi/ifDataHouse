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

        public InspectQueryModel(NameValueCollection collection)
        {
            
            this.TskNo = collection.Get("TskNo");
            this.LeoniNo = collection.Get("LeoniNo");
            this.CusNo = collection.Get("CusNo");
            this.ClipScanNo = collection.Get("ClipScanNo");
            if (collection.Get("ClipScanTime1Start") != null && collection.Get("ClipScanTime1Start").Trim().Length > 0)
            {
                this.ClipScanTime1Start = DateTime.Parse(collection.Get("ClipScanTime1Start"));
            }
            if (collection.Get("ClipScanTime1End") != null && collection.Get("ClipScanTime1End").Trim().Length > 0)
            {
                this.ClipScanTime1End = DateTime.Parse(collection.Get("ClipScanTime1End"));
            }
            if (collection.Get("ClipScanTime2Start") != null && collection.Get("ClipScanTime2Start").Trim().Length > 0)
            {
                this.ClipScanTime2Start = DateTime.Parse(collection.Get("ClipScanTime2Start"));
            }
            if (collection.Get("ClipScanTime2End") != null && collection.Get("ClipScanTime2End").Trim().Trim().Length > 0)
            {
                this.ClipScanTime2End = DateTime.Parse(collection.Get("ClipScanTime2End"));
            }
            this.TskScanNo = collection.Get("TskScanNo");
            if (collection.Get("TskScanTime3Start") != null && collection.Get("TskScanTime3Start").Trim().Length > 0)
            {
                this.TskScanTime3Start = DateTime.Parse(collection.Get("TskScanTime3Start"));
            }
            if (collection.Get("TskScanTime3End") != null && collection.Get("TskScanTime3End").Trim().Length > 0)
            {
                this.TskScanTime3End = DateTime.Parse(collection.Get("TskScanTime3End"));
            }

            if (collection.Get("Time3MinTime2Start") != null && collection.Get("Time3MinTime2Start").Trim().Length > 0)
            {
                float i = 0;
                if (float.TryParse(collection.Get("Time3MinTime2Start"), out i))
                {
                    this.Time3MinTime2Start = i;
                }
                else {
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
                this.createdAtStart = DateTime.Parse(collection.Get("createdAtStart"));
            }
            if (collection.Get("createdAtEnd") != null && collection.Get("createdAtEnd").Trim().Length > 0)
            {
                this.createdAtEnd = DateTime.Parse(collection.Get("createdAtEnd"));
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
            get { return CreatedAtEnd; }
            set { CreatedAtEnd = value; }
        }

    }
}