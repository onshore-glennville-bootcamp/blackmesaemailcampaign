using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace BlackMesaEmailCampaign.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(MarketManLoginFM credientials)
        {
            MarketManLoginVM user = new MarketManService().MarketManLogin(credientials);
            if (user != null)
            {
                Session["ID"] = user.ID;
                Session["Name"] = user.Email;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Username or Password incorrect.";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["ID"] = null;
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(MarketManRegisterFM registerFM)
        {
            MarketManService users = new MarketManService();
            MarketManLoginFM login = new MarketManLoginFM();
            if (registerFM.Email != null && users.ValidEmail(registerFM.Email))
            {
                if (users.ConfirmEmailLength(registerFM))
                {
                    if (users.IsValidUser(registerFM))
                    {
                        if (registerFM.Password != null && registerFM.Password.Length > 7 && registerFM.Password.Length < 26 && registerFM.Password == registerFM.ConfirmPassword)
                        {
                            users.CreateUser(registerFM);
                            login.Email = registerFM.Email;
                            login.Password = registerFM.Password;
                            MarketManLoginVM user = new MarketManService().MarketManLogin(login);
                            Session["ID"] = user.ID;
                            Session["Name"] = user.Email;
                            return RedirectToAction("Index", "Home");
                        }
                        ViewBag.ErrorMessage = "Passwords must be more than seven characters, less than 25 characters, and match.";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Email already exists.";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid email.";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Email not valid.";
            }
            return View();
        }
    }
}