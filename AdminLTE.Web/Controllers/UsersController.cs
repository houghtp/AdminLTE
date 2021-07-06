using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminLTE.Web.Models;
using DataTables;

namespace AdminLTE.Web.Controllers
{
    public class UsersController : Controller
    {
        private AdminLTEEntities db = new AdminLTEEntities();

        // GET: Tables
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Table()
        //{
        //    try
        //    {
        //        List<user> domains = db.users.ToList<user>();
        //        return Json(new { data = domains }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public ActionResult Table()
        {
            var request = System.Web.HttpContext.Current.Request;
            //var settings = Properties.Settings.Default;

            using (var editorDb = new DataTables.Database("sqlserver", db.Database.Connection))
            {

                var response = new Editor(editorDb, "users")
                    .Model<AdminLTEEntities>()
                        .Field(new Field("Id").Set(false))
                        .Process(request)
                    .Data();

                return Json(response);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
