using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSDemo.Models
{
    public class PagedCollection<T>
    {
        public int Page { get; set; }

        public int Count
        {
            get
            {
                return (null != this.items) ? this.items.Count() : 0;
            }
        }

        public int TotalPages { get; set; }
        public int totalItems { get; set; }

        public IEnumerable<T> items { get; set; }
    }
    
}