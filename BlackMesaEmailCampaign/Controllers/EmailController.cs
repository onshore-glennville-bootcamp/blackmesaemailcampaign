using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace BlackMesaEmailCampaign.Controllers
{
    public class EmailController : Controller
    {
        [HttpPost]
        public ActionResult Index(SubscribersVM selectedSubscribers)
        {
            return View(selectedSubscribers);
        }
    }
}