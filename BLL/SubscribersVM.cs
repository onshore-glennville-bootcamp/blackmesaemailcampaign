using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SubscribersVM
    {
        public SearchFM Search { get; set; }
        public List<SubscriberVM> Subscribers { get; set; }
        public SubscribersVM()
        {
            Search = new SearchFM();
            Subscribers = new List<SubscriberVM>();
        }
    }
}