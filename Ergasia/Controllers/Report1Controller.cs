using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ergasia.Models;

namespace Ergasia.Controllers
{
    public class Report1Controller : Controller
    {
        public ActionResult Index(DateTime? date_from, DateTime? date_to, int t=100)
        {

            string mindt_string = "04/01/1980 00:00:00 GMT+0530 (India Standard Time)";
            DateTime maxdt = DateTime.Now;
            DateTime mindt;
            DateTime.TryParseExact(mindt_string, "mm/dd/yyyy hh:mm:ss 'GMT'zzz '(India Standard Time)'", CultureInfo.InvariantCulture, DateTimeStyles.None, out mindt);

            pubsEntities db = new pubsEntities();
            List<authors> authorsname = db.authors.ToList();
            List<titleauthor> titleauthorsname = db.titleauthor.ToList();
            List<titles> titlesname = db.titles.ToList();

            var m = (from a in authorsname
                    join b in titleauthorsname on a.au_id equals b.au_id
                    join c in titlesname on b.title_id equals c.title_id
                    where (c.pubdate >= date_from && c.pubdate<= date_to) || (c.pubdate >= date_from && c.pubdate <= maxdt) ||(c.pubdate >= mindt && c.pubdate <= date_to) || (date_from == null && date_to == null)
                    orderby c.ytd_sales descending
                    select new Report1 { authorsdetails = a, titleauthordetails = b, titledetails = c }).Take(t);

            return View(m.ToList());

        }
    }
}