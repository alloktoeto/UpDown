using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpDown.Repository;
using UpDown.Models;

namespace UpDown.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/


        [HttpGet]
        public ActionResult Index()
        {
            if (Session["userLogin"] != null)
            {
                user uId = Session["userLogin"] as user;
                UsersRepository ur = new UsersRepository();
                user user = ur.GetUserByEmail(uId.email);
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
