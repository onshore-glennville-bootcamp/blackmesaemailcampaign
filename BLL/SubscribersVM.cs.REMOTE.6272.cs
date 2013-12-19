using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SubscribersVM
    {
        public List<SubscriberVM> Subscribers { get; set; }
        public SubscribersVM()
        {
            Subscribers = new List<SubscriberVM>();
        }

    }
}
