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
        public ActionResult Login(MarketManLoginFM credientials)
        {
            MarketManLoginVM user = new MarketManLoginService().MarketManLogin(credientials);
            if (user != null)
            {
                Session["ID"] = user.ID;
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
    }
}