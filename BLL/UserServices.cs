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
            SubscriberDAO dao = new SubscriberDAO();
            Subscribers subscriber = new Subscribers();
            subscriber.Email = subscriberFM.Email;
            subscriber.FirstName = subscriberFM.FirstName;
            subscriber.LastName = subscriberFM.LastName; dao.CreateSubscriber(subscriber);
        }
    }
}
