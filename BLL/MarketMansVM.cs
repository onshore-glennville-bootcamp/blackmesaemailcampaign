using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MarketMansVM
    {
        public List<MarketManVM> MarketingManagers { get; set; }
        public MarketMansVM()
        {
            MarketingManagers = new List<MarketManVM>();
        }
    }
}
