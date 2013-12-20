using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class MarketManFM
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public MarketManFM(MarketMan user)
        {
            this.Password = user.Password;
            this.Email = user.Email;
            this.ID = user.ID;
        }
        public MarketManFM ()
	    {

	    }

    }
}