using System;
using System.Collections.Generic;
using System.Linq;
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
            MarketManLoginVM user = new MarketManLoginService().MarketManLogin(credientials);
            if (user != null)
            {
                Session["ID"] = user.ID;
                Session["Name"] = user.Email;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Username or Password incorrect.";
                return View("Index");
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
            if (registerFM.Email != null && users.ValidEmail(registerFM.Email))
            {
                if (users.IsValidUser(registerFM))
                {
                    if (registerFM.Password != null && registerFM.Password.Length > 7 && registerFM.Password.Length < 26 && registerFM.Password == registerFM.ConfirmPassword)
                    {
                        users.CreateUser(registerFM);
                        ViewBag.ErrorMessage = "User Created";
                        return View();
                    }
                    ViewBag.ErrorMessage = "Password incorrect";
                }
                else
                {
                    ViewBag.ErrorMessage = "Email already exists.";
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