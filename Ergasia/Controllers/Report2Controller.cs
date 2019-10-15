using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ergasia.Models;

namespace Ergasia.Controllers
{
    public class Report2Controller : Controller
    {
        // GET: Report2
        public ActionResult Index(DateTime? date_from, DateTime? date_to, string store)
        {
            string mindt_string = "04/01/1980 00:00:00 GMT+0530 (India Standard Time)";
            DateTime maxdt = DateTime.Now;
            DateTime mindt;
            DateTime.TryParseExact(mindt_string, "mm/dd/yyyy hh:mm:ss 'GMT'zzz '(India Standard Time)'", CultureInfo.InvariantCulture, DateTimeStyles.None, out mindt);


            pubsEntities db = new pubsEntities();

            List<stores> storesname = db.stores.ToList();
            List<sales> salesname = db.sales.ToList();
            List<titles> titlesname = db.titles.ToList();

            var m = from a in storesname
                    join b in salesname on a.stor_id equals b.stor_id
                    join c in titlesname on b.title_id equals c.title_id
                    where ((b.ord_date >= date_from && b.ord_date <= date_to && a.stor_name.Contains(store)) || (b.ord_date >= date_from && b.ord_date <= maxdt && a.stor_name.Contains(store)) || (b.ord_date >= mindt && b.ord_date <= date_to && a.stor_name.Contains(store)) || (date_from == null && date_to == null && store == null))
                    select new Report2 { storesdetails = a, salesdetails = b, titledetails = c };

            return View(m.ToList());
        }
    }
}