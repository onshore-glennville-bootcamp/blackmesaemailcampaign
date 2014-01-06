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
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }
    }
}
