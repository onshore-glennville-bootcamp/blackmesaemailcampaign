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
        // Edit subscriber groups
        public ActionResult EditGroups()
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

        //View Needs work
        //Partial View for Viewing Group's Subscirbers
        public ActionResult _ViewGroupSubscribers(GroupVM group)
        {
            return PartialView(group);
        }

        //Needs Code
        //Partial View for Viewing All Subscribers with Action to add to group
        public ActionResult _AllSubscribers()
        {
            return PartialView();
        }

        //Need Code to Delete Group
        //Deletes Group and Redirects to View Groups
        public ActionResult Delete(GroupVM group)
        {
            return RedirectToAction("ViewGroups");
        }



        // Create subscriber groups
        [HttpGet]
        public ActionResult CreateGroups()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGroups(GroupFM groups)
        {
            GroupServices create = new GroupServices();
            create.CreateGroup(groups);
            return View();
        }
    }
}
