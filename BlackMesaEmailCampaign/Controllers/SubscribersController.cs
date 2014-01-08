using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BLL;

namespace BlackMesaEmailCampaign.Controllers
{
    public class SubscribersController : Controller
    {
        //Gets Form for Adding Subscribers
        [HttpGet]
        public ActionResult Add()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        //Validates Form for Adding Subscribers, then adds if validated
        [HttpPost]
        public ActionResult Add(SubscribersFM subscriber)
        {
            UserServices log = new UserServices();
            if (subscriber.Email != null && log.ValidEmail(subscriber.Email))
            {
                if (!log.IsExistingSubscriber(subscriber.Email))
                {
                    if (!log.TooLong(subscriber.FirstName) && !log.TooLong(subscriber.LastName))
                    {
                        log.CreateSubscribers(subscriber);
                        return RedirectToAction("ViewSubscribers");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "First/Last Name cannot be longer than 100 characters.";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Subscriber email already exists.";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Subscriber email not valid.";
            }
            return View();
        }
        //List Subscribers
        [HttpGet]
        public ActionResult AddFromFile()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            GroupServices log = new GroupServices();
            List<GroupVM> list = log.GetAllGroups();
            return View(list);
        }
        //Controller for adding bulk users from file
        [HttpPost]
        public ActionResult AddFromFile(HttpPostedFileBase file, int groupID)
        {
            UserServices log = new UserServices();
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                string ext = Path.GetExtension(file.FileName);
                StreamReader reader = new StreamReader(file.InputStream);
                while (!reader.EndOfStream)
                {
                    ViewBag.Subscribers = log.AddFromFile(reader, ext);
                }
                reader.Close();
            }
            return View();
        }
        public ActionResult ViewSubscribers()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            UserServices userS = new UserServices();
            SubscribersVM subscriber = new SubscribersVM();
            subscriber.Subscribers = userS.SortByLastName(userS.GetAllSubscribers());
            ViewBag.Sort = "LastName";
            return View(subscriber);
        }
        //Sort list of subscribers by last name, reverse if already sorted by last name
        public ActionResult ViewSubscribersByLastName(string sort)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            UserServices userS = new UserServices();
            SubscribersVM subscriber = new SubscribersVM();
            subscriber.Subscribers = userS.SortByLastName(userS.GetAllSubscribers());
            if (sort == "LastName")
            {
                ViewBag.Sort = "";
                subscriber.Subscribers.Reverse();
                return View("ViewSubscribers", subscriber);
            }
            ViewBag.Sort = "LastName";
            return View("ViewSubscribers", subscriber);
        }
        //Sort list of subscribers by first name, reverse if already sorted by first name
        public ActionResult ViewSubscribersByFirstName(string sort)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            UserServices userS = new UserServices();
            SubscribersVM subscriber = new SubscribersVM();
            subscriber.Subscribers = userS.SortByFirstName(userS.GetAllSubscribers());
            if (sort == "FirstName")
            {
                ViewBag.Sort = "";
                subscriber.Subscribers.Reverse();
                return View("ViewSubscribers", subscriber);
            }
            ViewBag.Sort = "FirstName";
            return View("ViewSubscribers", subscriber);
        }
        //Search Subscribers
        [HttpGet]
        public ActionResult SearchSubscribers()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult SearchSubscribers(string searchString)
        {
            UserServices userS = new UserServices();
            SubscribersVM subscriber = new SubscribersVM();
            subscriber.Subscribers = userS.Search(searchString);
            //and then we finish jumping through hoops...
            return View("ViewSubscribers", subscriber);
        }
        [HttpGet]
        public ActionResult Email()
        {
            return View();
        }
        //Gets list of checked Subscribers.  Needs code for emailing them.
        [HttpPost]
        public ActionResult Email(SubscribersVM selectedSubscribers)
        {
            UserServices log = new UserServices();
            return View("EmailSubscribers", log.Checked(selectedSubscribers.Subscribers));
        }
    }
}