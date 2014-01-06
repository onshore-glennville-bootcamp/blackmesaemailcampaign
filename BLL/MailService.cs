using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MailService
    {
        public MailFM GetFM(SubscribersVM subVM)
        {
            MailFM mailFM = new MailFM();
            foreach (SubscriberVM sub in subVM.Subscribers)
            {
                if (sub.EmailList)
                {
                    mailFM.To = mailFM.To + sub.Email + ", ";
                }
            }
            if (mailFM.To!= "")
            {
                mailFM.To = mailFM.To.Substring(0, mailFM.To.Length - 2);
                return mailFM;
            }
            return mailFM;
        }
    }
}
