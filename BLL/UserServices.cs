using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

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
        public void CreateSubscribers(SubscribersFM subscriberFM)
        {
            if (!IsExistingSubscriber(subscriberFM.Email))
            {
                SubscriberDAO dao = new SubscriberDAO();
                Subscribers subscriber = new Subscribers();
                subscriber.Email = subscriberFM.Email;
                subscriber.FirstName = subscriberFM.FirstName;
                subscriber.LastName = subscriberFM.LastName; dao.CreateSubscriber(subscriber);
            }
        }
        //Add list of subscribers to database
        public void CreateSubscribers(List<SubscribersFM> subscribers)
        {
            foreach (SubscribersFM subscriber in subscribers)
            {
                CreateSubscribers(subscriber);
            }
        }
        //Returns true if the email is in the right format
        public bool ValidEmail(string email)
        {
            if (!TooLong(email))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
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
            vm.Email = subscriber.Email;
            vm.LastName = subscriber.LastName;
            vm.FirstName = subscriber.FirstName;
            return vm;
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
        //Sort a list of subscribers by email
        public List<SubscriberVM> SortByEmail(List<SubscriberVM> list)
        {
            List<SubscriberVM> sorted = new List<SubscriberVM>();
            list.Sort((a, b) => a.Email.CompareTo(b.Email));
            foreach (SubscriberVM vm in list)
            {
                sorted.Add(vm);
            }
            return sorted;
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
    }
}