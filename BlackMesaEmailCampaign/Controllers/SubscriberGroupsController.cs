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
        //Partial View Group's Subscribers
        public ActionResult ViewGroupSubscribers(GroupVM group)
        {
            //Needs code for Viewing Subscribers in group
            GroupServices log = new GroupServices();
            SubscribersVM vm = new SubscribersVM();
            foreach (GroupVM groups in log.GetAllGroups())
            {
                if (groups.GroupName == group.GroupName)
                {
                    vm.Subscribers = groups.Subscribers;
                }
            }
            return PartialView("_ViewSubscribers", vm);
        }

        //Need View
        //Edit Group's Subscribers
        [HttpGet]
        public ActionResult Edit(GroupVM vm)
        {
            GroupFM group = new GroupFM();
            group.ID = vm.ID;
            group.GroupName = vm.GroupName;
            group.Subscribers = vm.Subscribers;
            return View(group);
        }
        [HttpPost]
        public ActionResult Edit(GroupFM group)
        {
            GroupServices log = new GroupServices();
            //Need code for editing group
            log.EditGroupSubscribers(group);
            return View("Edit");
        }

        //Need Code to Delete Group
        //Deletes Group and Redirects to Edit Groups
        public ActionResult Delete(GroupVM group)
        {
            //Needs code for deleting groups
            return RedirectToAction("Edit");
        }

    }
}