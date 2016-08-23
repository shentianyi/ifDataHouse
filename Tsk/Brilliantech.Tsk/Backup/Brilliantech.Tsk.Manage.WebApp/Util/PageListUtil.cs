using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brilliantech.Tsk.Manage.WebApp.Util
{
    public class PageListUtil<T> : List<T>
    {
        public int PageIndex { get; private set; }//当前页号减一的值 
        public int PageSize { get; private set; }　//每页显示的内容数量 
        public int TotalPages { get; private set; }//总页数 
        public int Start { get; private set; }//当前页面，显示的第一个页号（比如在中间的页面，页号显示是9号到16号，9就是Start） 
        public int End { get; private set; }//当前页面，显示的最后一个页号 

        public PageListUtil(List<T> source, int pageIndex, int pageSize, int totalPages)
        {
            this.PageIndex = pageIndex;
            this.PageSize = PageSize;
            this.TotalPages = totalPages;

            int size;
            if (this.TotalPages > 10)
            {
                size = 10;
                if (this.PageIndex > 2 && (this.PageIndex <= this.TotalPages - (size - 2)))
                {
                    this.Start = this.PageIndex - 1;
                }
                else if (this.PageIndex > this.TotalPages - (size - 2))
                {
                    this.Start = this.TotalPages - size + 1;
                }
                else
                {
                    this.Start = 1;
                }
            }
            else
            {
                size = this.TotalPages;
                this.Start = 1;
            }

            this.End = this.Start + size - 1;

            this.AddRange(source);
        }

        public bool HasPreviousPage
        {
            get { return this.PageIndex > 0; }
        }

        public bool HasNextPage
        {
            get { return this.PageIndex + 1 < this.TotalPages; }
        }
    }
}