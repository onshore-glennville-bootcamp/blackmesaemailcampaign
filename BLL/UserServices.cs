using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using DAL;
using System.IO;

namespace BLL
{
    public class UserServices
    {
        //Returns true if an email is already in the subscriber's table of the database
        public bool IsExistingSubscriber(string email)
        {
            SubscriberDAO dao = new SubscriberDAO();
            List<Subscribers> subscribers = dao.GetAllSubscribers();
            foreach (Subscribers subscriber in subscribers)
            {
                if (subscriber.Email == email)
                {
                    return true;
                }
            }
            return false;
        }
        //Add subscriber to database
        public void CreateSubscribers(SubscribersFM subscriberFM)
        {
            if (!IsExistingSubscriber(subscriberFM.Email))
            {
                SubscriberDAO dao = new SubscriberDAO();
                Subscribers subscriber = new Subscribers();
                subscriber.Email = subscriberFM.Email;
                subscriber.FirstName = subscriberFM.FirstName;
                subscriber.LastName = subscriberFM.LastName; dao.CreateSubscriber(subscriber);
            }
        }
        //Add list of subscribers to database
        public void CreateSubscribers(List<SubscribersFM> subscribers)
        {
            foreach (SubscribersFM subscriber in subscribers)
            {
                CreateSubscribers(subscriber);
            }
        }
        //Returns true if the email is in the right format
        public bool ValidEmail(string email)
        {
            if (!TooLong(email))
            {
                try
                {
                    var addr = new MailAddress(email);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        //Converts from Subscribers to SubscriberVM
        public SubscriberVM ConvertSubscriber(Subscribers subscriber)
        {
            SubscriberVM vm = new SubscriberVM();
            vm.Email = subscriber.Email;
            vm.LastName = subscriber.LastName;
            vm.FirstName = subscriber.FirstName;
            return vm;
        }
        //Gets a list of all subscribers in database
        public List<SubscriberVM> GetAllSubscribers()
        {
            List<SubscriberVM> subscriberVM = new List<SubscriberVM>();
            SubscriberDAO dao = new SubscriberDAO();
            List<Subscribers> subscribers = dao.GetAllSubscribers();
            foreach (Subscribers subscriber in subscribers)
            {
                subscriberVM.Add(ConvertSubscriber(subscriber));
            }
            return subscriberVM;
        }
        //Sort a list of subscribers by last name
        public List<SubscriberVM> SortByLastName(List<SubscriberVM> list)
        {
            List<SubscriberVM> sorted = new List<SubscriberVM>();
            list.Sort((a, b) => a.LastName.CompareTo(b.LastName));
            foreach (SubscriberVM vm in list)
            {
                sorted.Add(vm);
            }
            return sorted;
        }
        //Sort a list of subscribers by first name
        public List<SubscriberVM> SortByFirstName(List<SubscriberVM> list)
        {
            List<SubscriberVM> sorted = new List<SubscriberVM>();
            list.Sort((a, b) => a.FirstName.CompareTo(b.FirstName));
            foreach (SubscriberVM vm in list)
            {
                sorted.Add(vm);
            }
            return sorted;
        }
        //Checks for lengths over 100
        public bool TooLong(string name)
        {
            if (name != null && name.Length > 100)
            {
                return true;
            }
            return false;
        }
        //Get Subscriber By Email
        public SubscriberVM SubscriberByEmail(string email)
        {
            SubscriberDAO dao = new SubscriberDAO();
            return ConvertSubscriber(dao.GetSubscriberByEmail(email));
        }
        // Search Subscribers
        public List<SubscriberVM> Search(string s)
        {
            List<SubscriberVM> subscribersVM = new List<SubscriberVM>();
            // do some other stuff
            s = "%" + s + "%";
            SubscriberDAO dao = new SubscriberDAO();
            List<Subscribers> subscribers = dao.Search(s);
            foreach (Subscribers subscriber in subscribers)
            {
                subscribersVM.Add(ConvertSubscriber(subscriber));
            }
            return subscribersVM;
        }
        public List<SubscribersFM> SeparateXML(StreamReader stream)
        {
            List<string> AllLines = new List<string>();
            while (stream.Peek() >= 0)
            {
                AllLines.Add(st.ReadLine());
            }
            List<string> LostTags = new List<string>();
            //get rid of the tags
            for (int a = 0; a < AllLines.Count; a++)
            {
                if (AllLines[a].Substring(0, 1) != "<" && AllLines[a].Substring(0, 2) != "\t<")
                {
                    AllLines[a] = DumpTags(AllLines[a].Trim());
                    LostTags.Add(AllLines[a]);
                }
            }
            int g = 0;
            // create a list of subscribers
            List<SubscribersFM> Subscribers = new List<SubscribersFM>();
            for (int subscribersCount = 0; subscribersCount < AllLines.Count; subscribersCount += 3) ;
            {

            }
            //foreach (string item in LostTags)
            //{
            //    g = g + 1;
            //    Console.Write(item + " ");
            //    if (g % 3 == 0)
            //    {
            //        Console.WriteLine();
            //    }
            //}
            Console.ReadLine();

            //else
            //{
            //    Console.WriteLine("Please check your filename and try again");
            //    Console.ReadLine();
            //}
        }
        private static string DumpTags(string original)
        {
            int a = original.IndexOf(">") + 1; original = original.Substring(a);
            a = original.IndexOf("<");
            original = original.Substring(0, a);
            return original;
        }
        public List<SubscribersFM> SeparateCSV(StreamReader stream)
        {
            int linecheck = 0;
            string line = "";
            List<SubscribersFM> subscribers = new List<SubscribersFM>();
            while (line != null)
            {
                string subEmail = "", subFirstName = "", subLastName = "";
                line = stream.ReadLine();
                if (line == null) break;
                linecheck = line.IndexOf(',');
                subEmail = line.Substring(0, linecheck);
                line = line.Substring(linecheck + 1);
                linecheck = line.IndexOf(',');
                subFirstName = line.Substring(0, line.IndexOf(','));
                subLastName = line.Substring(line.IndexOf(',') + 1);
                subscribers.Add(new SubscribersFM { Email = subEmail, FirstName = subFirstName, LastName = subLastName });
            }
            return subscribers;
        }
        public string AddFromFile(StreamReader stream, string ext)
        {
            string uploaded = "File must be in CSV or XML format.  Fields should be in the order Email, First Name, Last Name";
            switch (ext)
            {
                case ".csv":
                    foreach (SubscribersFM fm in SeparateCSV(stream))
                    {
                        if (ValidEmail(fm.Email))
                        {
                            CreateSubscribers(fm);
                            uploaded = "Subscribers from CSV file were uploaded.";
                        }
                    }
                    return uploaded;
                case ".xml":
                    CreateSubscribers(SeparateCSV(stream));
                    return "Subscribers from XML file were uploaded.";
            }
            return uploaded;
        }
        //Pulls out unchecked subscribers and sends back list of checked subscribers
        public SubscribersVM Checked(SubscribersVM selectedSubscribers)
        {
            SubscribersVM selected = new SubscribersVM();
            foreach (SubscriberVM vm in selectedSubscribers.Subscribers)
            {
                if (vm.EmailList)
                {
                    selected.Subscribers.Add(SubscriberByEmail(vm.Email));
                }
            }
            return selected;
        }
        //sends emails out to subscribers
        public static string SendEmail(string from, string to)
        {
            try
            {
                from = "blackmesaemailcampaign@gmail.com";//Email we are using to send templates from
                to = "blackmesaemailcampaign@gmail.com";//this is the email to whom you want to send the template
                MailMessage mail = new MailMessage();
                mail.To.Add(to);
                mail.From = new MailAddress(from, "Black Mesa", Encoding.UTF8);
                mail.Subject = "This is a test mail";
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = "This is Email Body Text";
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(from, "bootcamp123");//bootcamp123 is the password for the email
                client.Port = 587;//Gmail works on this port
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;//Gmail works on Server Secured Layer
                client.Send(mail);
                return "Done";
            }
            catch (Exception e)
            {
                return e.Message;
            }// end try
        }
    }
}