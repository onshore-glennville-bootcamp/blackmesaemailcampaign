using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MarketingManagersVM
    {
        public List<MarketingManagerVM> MarketingManagers { get; set; }
        public MarketingManagersVM()
        {
            MarketingManagers = new List<MarketingManagerVM>();
        }
    }
}
