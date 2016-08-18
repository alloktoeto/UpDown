using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UpDown.Controllers
{
    public class ErrorsController : Controller
    {
        //
        // GET: /Errors/

        public ActionResult Error404()
        {
            return View();
        }


        public ActionResult Error()
        {
            return View();
        }
    }
}
