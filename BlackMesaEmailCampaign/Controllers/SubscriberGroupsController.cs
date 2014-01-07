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
        //Partial View All Subscribers
        public ActionResult AllSubscribers()
        {
            //Needs code for Viewing Subscribers in group
            UserServices log = new UserServices();
            SubscribersVM vm = new SubscribersVM();
            vm.Subscribers = log.GetAllSubscribers();
            return PartialView("_ViewSubscribers", vm);
        }
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
            GroupServices log = new GroupServices();
            GroupFM fm = new GroupFM();
            fm.ID = vm.ID;
            fm.GroupName = vm.GroupName;
            fm.Subscribers = log.GetSubscribersByGroupID(vm.ID);
            return View(fm);
        }
        [HttpPost]
        public ActionResult Add(GroupFM group)
        {
            GroupServices log = new GroupServices();
            log.DeleteGroupSubscribers(group);
            //Need code for editing group
            return View("Edit");
        }
        //Need Code to Delete Group
        //Deletes Group and Redirects to Edit Groups
        [HttpPost]
        public ActionResult Delete(GroupVM group)
        {
            //Needs code for deleting groups
            return RedirectToAction("Edit");
        }
    }
}