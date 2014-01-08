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
        //Edit Group's Subscribers
        public ActionResult Edit(GroupVM vm)
        {
            GroupServices log = new GroupServices();
            GroupFM fm = new GroupFM();
            fm.ID = vm.ID;
            fm.GroupName = vm.GroupName;
            fm.Subscribers = log.GetSubscribersByGroupID(vm.ID);
            return View(fm);
        }
        //Partial View for all subscribers in group
        public ActionResult GroupSubscribers(List<SubscriberVM> subscribers)
        {
            SubscribersVM members = new SubscribersVM();
            foreach (SubscriberVM vm in subscribers)
            {
                if (vm.EmailList)
                {
                    members.Subscribers.Add(vm);
                }
            }
            return PartialView("_GroupSubscribers", members);
        }
        //Update Subscribers in Group
        [HttpPost]
        public ActionResult UpdateSubscribers(GroupFM group)
        {
            GroupServices log = new GroupServices();
            log.UpdateGroupSubscribers(group);
            return RedirectToAction("Edit", group);
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