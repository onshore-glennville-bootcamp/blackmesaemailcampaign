using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class Groups
    {
        public int ID { get; set; }
        public string GroupName { get; set; }
        public List<Subscribers> Subscribers { get; set; }

    }
}
