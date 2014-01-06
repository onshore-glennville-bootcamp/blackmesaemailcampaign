using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlackMesaEmailCampaign.Controllers
{
    public class SubscriberGroupsController : Controller
    {
        //
        // GET: /SubscriberGroups/
        public ActionResult Index()
        {
            return View();
        }
        // Edit subscriber groups
        public ActionResult EditGroups()
        {
            return View();
        }
	}
}