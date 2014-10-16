using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace Brilliantech.Tsk.Manage.WebApp.Models
{
    public class InspectOriginQueryModelBAK
    {
        private string text;
        private bool? processResult;
        private DateTime? createdAtStart;
        private DateTime? createdAtEnd;
        private static List<string> csvHead = new List<string>() { 
         "数据文件内容","处理成功/失败","处理信息","数据上传时间"
        };


        private static List<string> fileds = new List<string>() { 
        "Text","ProcessResult","ProcessMessage","CreatedAtView"
        };

        public InspectOriginQueryModelBAK(NameValueCollection collection)
        {
            this.Text = collection.Get("Text");
            if (collection.Get("ProcessResult")!=null)
            {
                bool result = true;
                if (bool.TryParse(collection.Get("ProcessResult"), out result))
                {
                    this.ProcessResult = result;
                }
                else {
                    this.ProcessResult = null;
                }
            } 
            if (collection.Get("createdAtStart") != null && collection.Get("createdAtStart").Trim().Length > 0)
            {
                this.CreatedAtStart = DateTime.Parse(collection.Get("CreatedAtStart"));
            }
            if (collection.Get("CreatedAtEnd") != null && collection.Get("CreatedAtEnd").Trim().Length > 0)
            {
                this.CreatedAtEnd = DateTime.Parse(collection.Get("CreatedAtEnd"));
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = (value != null && value.Trim().Length > 0) ? value : null;
            }
        }

        public bool? ProcessResult
        {
            get { return processResult; }
            set { processResult = value; }
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
            get { return InspectOriginQueryModelBAK.csvHead; }
        }

        public static List<string> Fileds
        {
            get { return InspectOriginQueryModelBAK.fileds; }
        }

    }
}