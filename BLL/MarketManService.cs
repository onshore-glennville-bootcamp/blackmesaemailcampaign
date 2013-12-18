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
        public bool CreateUser(MarketManFM userFM)
        {
            if (IsValidUser(userFM))
            {
                //email temp pass to user
                MarketManDAO dao = new MarketManDAO();
                MarketMan user = new MarketMan();
                user.Email = userFM.Email;
                user.Password = userFM.Password;
                dao.CreateMarketMan(user);
                return true;
            }
            return false;
        }
        public bool IsValidUser(MarketManFM userFM)
        {
            MarketManDAO dao = new MarketManDAO();
            if (userFM.Email != null && userFM.Email.Length > 5 && dao.GetMarketManByEmail(userFM.Email) == null)
            {
                return true;
            }
            return false;
        }
        public MarketManFM GetUserFM(int ID)
        {
            MarketManDAO dao = new MarketManDAO();
            MarketMan user = dao.GetMarketManByID(ID);
            MarketManFM userFM = new MarketManFM(user);
            return userFM;
        }
        public void UpdateUser(MarketManFM userFM)
        {
            MarketManDAO dao = new MarketManDAO();
            MarketMan user = dao.GetMarketManByID(userFM.ID);
            user.Email = userFM.Email;
            dao.UpdateMarketMan(user);
        }
        public MarketManPassFM GetMarketManPassFM(int ID)
        {
            MarketManDAO dao = new MarketManDAO();
            MarketMan user = dao.GetMarketManByID(ID);
            MarketManPassFM passwordFM = new MarketManPassFM(user);
            return passwordFM;
        }
        public void UpdatePassword(MarketManPassFM passwordFM)
        {
            MarketManDAO dao = new MarketManDAO();
            MarketMan user = dao.GetMarketManByID(passwordFM.ID);
            user.Password = passwordFM.NewPassword;
            dao.UpdateMarketMan(user);
        }
        public void UpdateMarketMan(MarketManFM userFM)
        {
            MarketManDAO dao = new MarketManDAO();
            MarketMan user = dao.GetMarketManByID(userFM.ID);
            user.Email = userFM.Email;
            dao.UpdateMarketMan(user);
        }
        public void DeleteMarketMan(int ID)
        {
            MarketManDAO dao = new MarketManDAO();
            dao.DeleteMarketMan(ID);
        }
        public bool VerifyPassword(MarketManPassFM passwordFM)
        {
            if (passwordFM.CurrentPassword == GetMarketManPassFM(passwordFM.ID).CurrentPassword)
            {
                return true;
            }
            return false;
        }

    }
}