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
        public void CreateSubscribers(List<SubscribersFM> subscribers)
        {
            foreach (SubscribersFM subscriber in subscribers)
            {
                CreateSubscribers(subscriber);
            }
        }
        public bool ValidEmail(string email)
        {
            if (email.Length < 100)
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
        public SubscriberVM ConvertSubscriber(Subscribers subscriber)
        {
            SubscriberVM vm = new SubscriberVM();
            vm.Email = subscriber.Email;
            vm.LastName = subscriber.LastName;
            vm.FirstName = subscriber.FirstName;
            return vm;
        }
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
    }
}
