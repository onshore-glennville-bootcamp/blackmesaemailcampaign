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
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        // Edit subscriber groups
        public ActionResult EditGroups()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            GroupServices log = new GroupServices();
            log.UpdateGroupSubscribers(group);
            group.GroupName = log.GetGroupNameByID(group.ID);
            return RedirectToAction("Edit", group);
        }
        //Search Subscribers
        [HttpPost]
        public ActionResult _Search(int groupID, string search)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            GroupServices log = new GroupServices();
            GroupFM fm = new GroupFM();
            fm.ID = groupID;
            fm.GroupName = log.GetGroupNameByID(fm.ID);
            fm.Subscribers = log.GetSubscribersByGroupID(fm.ID);
            fm.Search = log.Search(fm.ID, search);
            return View("Edit", fm);
        }

        //Need Code to Delete Group
        //Deletes Group and Redirects to Edit Groups
        [HttpPost]
        public ActionResult Delete(GroupVM group)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Needs code for deleting groups
            return RedirectToAction("Edit");
        }


        // Create subscriber groups
        [HttpGet]
        public ActionResult CreateGroups()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateGroups(GroupFM groups)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            GroupServices create = new GroupServices();
            create.CreateGroup(groups);
            return View();
        }
    }
}
