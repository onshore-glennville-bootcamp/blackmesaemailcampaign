using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlackMesaEmailCampaign.Controllers
{
    public class SubscribersController : Controller
    {
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(SubscribersController subscribers)
        {
            return RedirectToRoute("Index", "Home");
        }
	}
}