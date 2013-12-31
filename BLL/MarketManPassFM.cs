using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class MarketManPassFM
    {
        public int ID { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string VerifyPassword { get; set; }
        public MarketManPassFM(MarketMan user)
        {
            this.CurrentPassword = user.Password;
            this.NewPassword = user.Password;
            this.VerifyPassword = user.Password;
            this.ID = user.ID;
        }
        public MarketManPassFM()
	    {

	    }

    }
}
