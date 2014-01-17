using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BLL
{
    public class GroupFM
    {
        public int ID { get; set; }

        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        public List<SubscriberVM> Subscribers { get; set; }

        public List<SubscriberVM> Search { get; set; }
    }
}
