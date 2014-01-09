using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using DAL;
using System.IO;

namespace BLL
{
    public class UserServices
    {
        //Returns true if an email is already in the subscriber's table of the database
        public bool IsExistingSubscriber(string email)
        {
            SubscriberDAO dao = new SubscriberDAO();
            List<Subscribers> subscribers = dao.GetAllSubscribers();
            foreach (Subscribers subscriber in subscribers)
            {
                if (subscriber.Email == email)
                {
                    return true;
                }
            }
            return false;
        }
        //Add subscriber to database
        public void CreateSubscribers(SubscribersFM subscriberFM, int groupID)
        {
            if (!IsExistingSubscriber(subscriberFM.Email) && !TooLong(subscriberFM.Email) && ValidEmail(subscriberFM.Email))
            {
                SubscriberDAO dao = new SubscriberDAO();
                GroupDAO group = new GroupDAO();
                Subscribers subscriber = new Subscribers();
                subscriber.Email = subscriberFM.Email;
                subscriber.FirstName = subscriberFM.FirstName;
                subscriber.LastName = subscriberFM.LastName; 
                dao.CreateSubscriber(subscriber);
                //0 is the groupID passed down if there is no group seleted.
                //if subscriber was created and group was seleted then it is add to a group
                if (groupID > 0 && dao.GetSubscriberByEmail(subscriber.Email) != null)
                {
                    group.AddGroupSubscribers(groupID, dao.GetSubscriberByEmail(subscriber.Email).ID);
                }
            }
        }
        //Add list of subscribers to database
        public void CreateSubscribers(List<SubscribersFM> subscribers, int groupID)
        {
            foreach (SubscribersFM subscriber in subscribers)
            {
                CreateSubscribers(subscriber, groupID);
            }
        }
        //Returns true if the email is in the right format
        public bool ValidEmail(string email)
        {
            if (!TooLong(email))
            {
                try
                {
                    var addr = new MailAddress(email);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        //Converts from Subscribers to SubscriberVM
        public SubscriberVM ConvertSubscriber(Subscribers subscriber)
        {
            SubscriberVM vm = new SubscriberVM();
            vm.ID = subscriber.ID;
            vm.Email = subscriber.Email;
            vm.LastName = subscriber.LastName;
            vm.FirstName = subscriber.FirstName;
            return vm;
        }
        //Converts from VM to Subscribers
        public Subscribers ConvertSubscriber(SubscriberVM vm)
        {
            Subscribers subscriber = new Subscribers();
            subscriber.ID = vm.ID;
            subscriber.Email = vm.Email;
            subscriber.LastName = vm.LastName;
            subscriber.FirstName = vm.FirstName;
            return subscriber;
        }
        //Gets a list of all subscribers in database
        public List<SubscriberVM> GetAllSubscribers()
        {
            List<SubscriberVM> subscriberVM = new List<SubscriberVM>();
            SubscriberDAO dao = new SubscriberDAO();
            List<Subscribers> subscribers = dao.GetAllSubscribers();
            foreach (Subscribers subscriber in subscribers)
            {
                subscriberVM.Add(ConvertSubscriber(subscriber));
            }
            return subscriberVM;
        }
        //Sort a list of subscribers by last name
        public List<SubscriberVM> SortByLastName(List<SubscriberVM> list)
        {
            List<SubscriberVM> sorted = new List<SubscriberVM>();
            list.Sort((a, b) => a.LastName.CompareTo(b.LastName));
            foreach (SubscriberVM vm in list)
            {
                sorted.Add(vm);
            }
            return sorted;
        }
        //Sort a list of subscribers by first name
        public List<SubscriberVM> SortByFirstName(List<SubscriberVM> list)
        {
            List<SubscriberVM> sorted = new List<SubscriberVM>();
            list.Sort((a, b) => a.FirstName.CompareTo(b.FirstName));
            foreach (SubscriberVM vm in list)
            {
                sorted.Add(vm);
            }
            return sorted;
        }
        //Checks for lengths over 100
        public bool TooLong(string name)
        {
            if (name != null && name.Length > 100)
            {
                return true;
            }
            return false;
        }
        //Get Subscriber By Email
        public SubscriberVM SubscriberByEmail(string email)
        {
            SubscriberDAO dao = new SubscriberDAO();
            return ConvertSubscriber(dao.GetSubscriberByEmail(email));
        }
        // Search Subscribers
        public List<SubscriberVM> Search(string s)
        {
            List<SubscriberVM> subscribersVM = new List<SubscriberVM>();
            // do some other stuff
            s = "%" + s + "%";
            SubscriberDAO dao = new SubscriberDAO();
            List<Subscribers> subscribers = dao.Search(s);
            foreach (Subscribers subscriber in subscribers)
            {
                subscribersVM.Add(ConvertSubscriber(subscriber));
            }
            return subscribersVM;
        }
        public List<SubscribersFM> SeparateXML(StreamReader stream)
        {
            List<string> AllLines = new List<string>();
            while (stream.Peek() >= 0)
            {
                AllLines.Add(stream.ReadLine());
            }
            List<string> LostTags = new List<string>();
            //get rid of the tags
            for (int a = 0; a < AllLines.Count; a++)
            {
                if (AllLines[a].Substring(0, 1) != "<" && AllLines[a].Substring(0, 2) != "\t<")
                {
                    AllLines[a] = DumpTags(AllLines[a].Trim());
                    LostTags.Add(AllLines[a]);
                }
            }
            // create a list of subscribers
            List<SubscribersFM> Subscribers = new List<SubscribersFM>();
            for (int subscribersCount = 0; subscribersCount < LostTags.Count; subscribersCount += 3)
            {
                Subscribers.Add(new SubscribersFM { Email = LostTags[subscribersCount], FirstName = LostTags[subscribersCount + 1], LastName = LostTags[subscribersCount + 2] });
            }
            return Subscribers;
        }
        private static string DumpTags(string original)
        {
            int a = original.IndexOf(">") + 1; original = original.Substring(a);
            a = original.IndexOf("<");
            original = original.Substring(0, a);
            return original;
        }
        public List<SubscribersFM> SeparateCSV(StreamReader stream)
        {
            int linecheck = 0;
            string line = "";
            List<SubscribersFM> subscribers = new List<SubscribersFM>();
            while (line != null)
            {
                string subEmail = "", subFirstName = "", subLastName = "";
                line = stream.ReadLine();
                if (line == null) break;
                linecheck = line.IndexOf(',');
                subEmail = line.Substring(0, linecheck);
                line = line.Substring(linecheck + 1);
                linecheck = line.IndexOf(',');
                subFirstName = line.Substring(0, line.IndexOf(','));
                subLastName = line.Substring(line.IndexOf(',') + 1);
                subscribers.Add(new SubscribersFM { Email = subEmail, FirstName = subFirstName, LastName = subLastName });
            }
            return subscribers;
        }
        public string AddFromFile(StreamReader stream, string ext, int groupID)
        {
            string uploaded = "File must be in CSV or XML format.  Fields should be in the order Email, First Name, Last Name";
            switch (ext)
            {
                case ".csv":
                    foreach (SubscribersFM fm in SeparateCSV(stream))
                    {
                        if (ValidEmail(fm.Email))
                        {
                            CreateSubscribers(fm, groupID);
                            uploaded = "Subscribers from CSV file were uploaded.";
                        }
                    }
                    return uploaded;
                case ".xml":
                    foreach (SubscribersFM fm in SeparateXML(stream))
                    {
                        if (ValidEmail(fm.Email))
                        {
                            CreateSubscribers(fm, groupID);
                            uploaded = "Subscribers from XML file were uploaded.";
                        }
                    }
                    
                    return "Subscribers from XML file were uploaded.";
            }
            return uploaded;
        }
        //Pulls out unchecked subscribers and sends back list of checked subscribers
        public SubscribersVM Checked(List<SubscriberVM> selectedSubscribers)
        {
            SubscribersVM selected = new SubscribersVM();
            foreach (SubscriberVM vm in selectedSubscribers)
            {
                if (vm.EmailList)
                {
                    selected.Subscribers.Add(SubscriberByEmail(vm.Email));
                }
            }
            return selected;
        }
    }
}