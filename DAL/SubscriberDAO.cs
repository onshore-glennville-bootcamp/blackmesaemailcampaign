using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
    public class SubscriberDAO
    {
        //Writes to database
        public void Write(string statement, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=EmailCampaign;Integrated Security=SSPI;"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    command.ExecuteNonQuery();
                }
            }
        }
        //Read Subscribers table of database
        public List<Subscribers> ReadSubscribers(string statement, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=EmailCampaign;Integrated Security=SSPI;"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SqlDataReader data = command.ExecuteReader();
                    List<Subscribers> subscribers = new List<Subscribers>();
                    while (data.Read())
                    {
                        Subscribers subscriber = new Subscribers();
                        subscriber.ID = Convert.ToInt32(data["ID"]);
                        subscriber.FirstName = data["FirstName"].ToString();
                        subscriber.LastName = data["LastName"].ToString();
                        subscriber.Email = data["Email"].ToString();
                        subscribers.Add(subscriber);
                    }
                    try
                    {
                        return subscribers;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }
        //Returns all subscribers
        public List<Subscribers> GetAllSubscribers()
        {
            return ReadSubscribers("GetAllSubscribers", null);
        }
        //Returns a subscriber when you input an email
        public Subscribers GetSubscriberByEmail(string email)
        {
            List<Subscribers> subscribers = GetAllSubscribers();
            foreach (Subscribers subscriber in subscribers)
            {
                if (subscriber.Email == email)
                {
                    return subscriber;
                }
            }
            return null;
        }
        //Add subscriber to database
        public void CreateSubscriber(Subscribers subscriber)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Email", subscriber.Email),
                new SqlParameter("@FirstName", subscriber.FirstName),
                new SqlParameter("@LastName", subscriber.LastName),
                new SqlParameter("@Active", 1)
            };
            Write("CreateSubscriber", parameters);
        }

        public List<Subscribers> Search(string s)
        {
            //retrieve list of users matching search parameters
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter ("@search", s)
            };
            return ReadSubscribers("SearchSubscribers", parameters);
            
        }
    }
}
