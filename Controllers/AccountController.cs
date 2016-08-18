using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UpDown.Models;
using UpDown.Repository;

namespace UpDown.Controllers
{
    public class AccountController : Controller
    {
        //private DataManager dm;
        public AccountController() { }
        //public AccountController(DataManager dm)
        //{
        //    this.dm = dm;
        //}


        [HttpGet]
        public ActionResult Index()
        {
            if (Session["userLogin"]!=null)
            {
                user uId = Session["userLogin"] as user;
                UsersRepository ur = new UsersRepository();
                user user = ur.GetUserByEmail(uId.email);
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //MembershipCreateStatus status = dm.MembershipProvider.CreateUser(model.UserName, model.Password, model.Email, model.FirstName, model.LastName, "s");
                //if (status == MembershipCreateStatus.Success)
                //{
                //    return View("Success");
                //}

                //ModelState.AddModelError("", GetMembershipCreateStatusResultText(status));
            }
            return View(model);
        }

        private string GetMembershipCreateStatusResultText(MembershipCreateStatus status)
        {
            if (status == MembershipCreateStatus.DuplicateEmail)
            {
                return "duplicate email";
            }

            if (status == MembershipCreateStatus.DuplicateUserName)
            {
                return "duplicate username";
            }

            return "register error";
        }

        public ActionResult LogOn()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            //FormsAuthentication.SignOut();
            Session["userLogin"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public bool LoginWithGPlus(string fname, string lname, string email, string from)
        {
            if (email == "" || email == null)
            {
                return false;
            }
            user user = new user
            {
                userID = Guid.NewGuid(),
                email = email,
                firstname = fname,
                lastname = lname,
                password = "",
                username = email,
                from = from
            };

            UsersRepository userRepository = new UsersRepository();

            try
            {
                userRepository.LoginSignUp(user);
                Session["userLogin"] = user;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost]
        public bool LoginWithFB(string fname, string lname, string email, string from)
        {
            if (email == "" || email == null)
            {
                return false;
            }
            user user = new user
            {
                userID = Guid.NewGuid(),
                email=email,
                firstname=fname,
                lastname=lname,
                password="",
                username=email,
                from = from
            };

            UsersRepository userRepository = new UsersRepository();

            try
            {
                userRepository.LoginSignUp(user);
                Session["userLogin"] = user;
                return true;
            }
            catch (Exception)
            {
                return false;
            }

            //dm = new DataManager(new UsersRepository(), new PrimaryMembershipProvider());
            //MembershipCreateStatus status = dm.MembershipProvider.CreateUser(user.Email, user.FirstName, user.LastName, user.Password, user.Email, from);
            ////MembershipCreateStatus status = dm.MembershipProvider.CheckEmail(email);
            //if (status == MembershipCreateStatus.Success)
            //{
            //    return "success";
            //}
            //else if (status == MembershipCreateStatus.DuplicateEmail)
            //{
            //    return "success";
            //}
            //else
            //{
            //    ModelState.AddModelError("", GetMembershipCreateStatusResultText(status));
            //    return "";
            //}

        }

        [HttpPost]
        public bool SignUp(string fname, string lname, string email, string password)
        {
            if (email == "" || email == null)
            {
                return false;
            }
            user user = new user
            {
                userID = Guid.NewGuid(),
                email = email,
                firstname = fname,
                lastname = lname,
                password = password,
                username = email,
                from=null
            };

            UsersRepository userRepository = new UsersRepository();

            try
            {
                userRepository.LoginSignUp(user);
                Session["userLogin"] = user;
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        [HttpPost]
        public bool Login(string email, string password)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                UsersRepository ur = new UsersRepository();
                user user = ur.GetUserByEmail(email);
                if (user==null)
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(user.from))
                {
                    if (user.password==password)
                    {
                        Session["userLogin"] = user;
                        return true;
                    }
                }
                if (user.password == password)
                {
                    Session["userLogin"] = user;
                    return true;
                }
            }
            return false;
        }

        public ActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAdmin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UsersRepository ur = new UsersRepository();
                user user = ur.GetUserByEmail(model.UserName);
                if (user == null)
                {
                    return View();
                }
                
                if (user.password == model.Password && user.idAdmin==true)
                {
                    Session["AdminLoginUser"] = user;
                    return RedirectToAction("Products", "Admin");
                }
                return View();
            }

            return View();
        }
    }
}
