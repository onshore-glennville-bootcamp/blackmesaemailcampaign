using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

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
        //View Groups
        public ActionResult ViewGroups()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            GroupServices log = new GroupServices();
            GroupsVM groups = new GroupsVM();
            groups.Groups = log.GetAllGroups();
            return View(groups);
        }

        //Need View
        //View Group's Subscribers
        public ActionResult ViewGroupSubscribers(GroupVM group)
        {
            return View(group);
        }

        //Need View
        //Edit Group's Subscribers
        public ActionResult Edit(GroupVM group)
        {
            return View(group);
        }

        //Need Code to Delete Group
        //View Group's Subscribers
        public ActionResult Delete(GroupVM group)
        {
            return RedirectToAction("ViewGroups");
        }

    }
}