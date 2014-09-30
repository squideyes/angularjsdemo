using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSDemo.Models
{
    public class LogItem
    {
        public int Id { get; set; }
        public string Module { get; set; }
        public DateTimeOffset? LoggedOn { get; set; }
        public string LogLevel { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }

}