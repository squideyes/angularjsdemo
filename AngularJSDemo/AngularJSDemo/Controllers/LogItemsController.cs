using AngularJSDemo.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Data.Entity;
using System.Data.SqlClient;

namespace AngularJSDemo.Controllers
{
    public class LogItemsController : ODataController
    {
        LogItemsContext db = new LogItemsContext();


        [EnableQuery]
        public List<LogItem> Get(int? pageNumber, int? pageSize, string sortInfo, string name)
        {
            //Skip elements
            int skip = 0;
            if (pageNumber >1 )
            {
                skip = (pageNumber.Value - 1) * pageSize.Value;
            }
                        
            if (sortInfo != null)
            {
                name = name == null ? "" : name;
                
                switch (sortInfo)
                {
                    case "+LogLevel":
                        var lstRec = (from c in db.LogItems
                                  where c.LogLevel.Contains(name) || c.UserName.Contains(name)
                                   select c).OrderBy(d => d.LogLevel).Skip(skip).Take(pageSize.Value).ToList();
                        return lstRec;
                        break;
                    case "-LogLevel":
                        var lstRec1 = (from c in db.LogItems
                                      where c.LogLevel.Contains(name) || c.UserName.Contains(name)
                                       select c).OrderByDescending(d => d.LogLevel).Skip(skip).Take(pageSize.Value).ToList();
                        return lstRec1;
                        break;
                    case "-UserName":
                        var lstRec2 = (from c in db.LogItems
                                      where c.LogLevel.Contains(name) || c.UserName.Contains(name)
                                       select c).OrderByDescending(d => d.UserName).Skip(skip).Take(pageSize.Value).ToList();                        
                        return lstRec2;
                        break;
                    case "+UserName":
                        var lstRec3 = (from c in db.LogItems
                                       where c.LogLevel.Contains(name) || c.UserName.Contains(name)
                                       select c).OrderBy(d => d.UserName).Skip(skip).Take(pageSize.Value).ToList();
                        return lstRec3;
                        break;
                    case "+Module":
                        var lstRec4 = (from c in db.LogItems
                                       where c.LogLevel.Contains(name) || c.UserName.Contains(name)
                                       select c).OrderBy(d => d.Module).Skip(skip).Take(pageSize.Value).ToList();
                        return lstRec4;
                        break;
                    case "-Module":
                        var lstRec5 = (from c in db.LogItems
                                       where c.LogLevel.Contains(name) || c.UserName.Contains(name)
                                       select c).OrderByDescending(d => d.Module).Skip(skip).Take(pageSize.Value).ToList();
                        return lstRec5;
                        break;

                    case "+Id":
                        var lstRec6 = (from c in db.LogItems
                                       where c.LogLevel.Contains(name) || c.UserName.Contains(name)
                                       select c).OrderBy(d => d.Id).Skip(skip).Take(pageSize.Value).ToList();
                        return lstRec6;
                        break;
                    case "-Id":
                        var lstRec7 = (from c in db.LogItems
                                       where c.LogLevel.Contains(name) || c.UserName.Contains(name)
                                       select c).OrderByDescending(d => d.Id).Skip(skip).Take(pageSize.Value).ToList();
                        return lstRec7;
                        break;
                }
            }
            List<LogItem> lstFiltered = db.LogItems.OrderBy(d => d.Id).Skip(skip).Take(pageSize.Value).ToList();
            return lstFiltered;
        }

        [EnableQuery]
        public SingleResult<LogItem> Get([FromODataUri] int key)
        {
            IQueryable<LogItem> result = db.LogItems.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

        private bool LogItemExists(int key)
        {
            return db.LogItems.Any(p => p.Id == key);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}