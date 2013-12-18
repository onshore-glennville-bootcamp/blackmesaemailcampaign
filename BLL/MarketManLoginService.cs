using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class MarketManLoginService
    {
        public MarketManLoginVM MarketManLogin(MarketManLoginFM login)
        {
            MarketManLoginVM userVM = null;
            MarketManDAO dao = new MarketManDAO();
            MarketMan user = dao.GetMarketManByEmail(login.Email);
            if (user != null && user.Password == login.Password)
            {
                userVM = new MarketManLoginVM();
                userVM.Email = user.Email;
                userVM.ID = user.ID;
            }
            return userVM;
        }
    }
}
