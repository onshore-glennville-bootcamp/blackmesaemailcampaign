using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class MarketManService
    {
        public MarketMansVM GetMarketMans()
        {
            MarketManDAO dao = new MarketManDAO();
            List<MarketMan> users = dao.GetAllMarketMans();
            MarketMansVM usersVM = new MarketMansVM();
            foreach (MarketMan user in users)
            {
                MarketManVM userVM = new MarketManVM();
                userVM.ID = user.ID;
                userVM.Email = user.Email;
                usersVM.MarketingManagers.Add(userVM);
            }
            return usersVM;
        }
        public bool IsValidUser(string email)
        {
            MarketManDAO dao = new MarketManDAO();
            List<MarketMan> users = dao.GetAllMarketMans();
            foreach (MarketMan user in users)
            {
                if (user.Email == email)
                {
                    return true;
                }
            }
            return false;
        }
        public void CreateMarketMan(MarketManFM userFM)
        {
            if (!IsValidUser(userFM.Email))
            {
                MarketManDAO dao = new MarketManDAO();
                MarketMan user = new MarketMan();
                user.Email = userFM.Email;
                user.Password = userFM.Password;
                dao.CreateMarketMan(user);
            }
        }
        public bool ValidEmail(string email)
        {
            if (email.Length < 100)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        //public MarketManFM GetMarketManFM(int ID)
        //{
        //    MarketManDAO dao = new MarketManDAO();
        //    MarketMan user = dao.GetMarketManByID(ID);
        //    MarketManFM userFM = new MarketManFM(user);
        //    return userFM;
        //}
    }
}