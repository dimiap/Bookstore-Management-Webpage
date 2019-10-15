using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ergasia.Models;

namespace Ergasia.Controllers
{
    public class BackUpController : Controller
    {
        private pubsEntities db = new pubsEntities();
        // GET: BackUp
        public ActionResult BackupDatabase()
        {
            string dbPath = Server.MapPath("~/BackupDB/DBBackup.bak");
            using (var db = new pubsEntities())
            {
                var cmd = String.Format("BACKUP DATABASE {0} TO DISK='{1}' WITH FORMAT, MEDIANAME='DbBackups', MEDIADESCRIPTION='Media set for {0} database';"
                , "pubs", dbPath);
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd);
            }
            return new FilePathResult(dbPath, "application/octet-stream");
        }

        public ActionResult RestoreDatabase()
        {
            string dbPath = Server.MapPath("~/BackupDB/DBBackup.bak");
            using (var db = new pubsEntities())
            {

                var cmd = String.Format("USE master restore DATABASE pubs from DISK='{0}' WITH REPLACE;", dbPath);
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd);
            }
            return View();
        }
    }
}