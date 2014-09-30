using System.Data.Entity;

namespace AngularJSDemo.Models
{
    public class LogItemsContext : DbContext
    {
        public LogItemsContext()
            : base("name=LogItemsContext")
        {
        }
        public DbSet<LogItem> LogItems { get; set; }
    }
}