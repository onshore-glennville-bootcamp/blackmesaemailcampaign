using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
    class MarketingManagerDAO
    {
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
        public List<MarketingManager> ReadMarketingManagers(string statement, SqlParameter[] parameters)
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
                    List<MarketingManager> subscribers = new List<MarketingManager>();
                    while (data.Read())
                    {
                        MarketingManager subscriber = new MarketingManager();
                        subscriber.ID = Convert.ToInt32(data["ID"]);
                        subscriber.Email = data["Email"].ToString();
                        subscriber.Password = data["Password"].ToString();
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
        public void CreateUser(MarketingManager user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Active", 1)
            };
            Write("CreateMarketingManager", parameters);
        }
        public MarketingManager GetMarketingManagerByEmail(string email)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email)
            };
            return ReadMarketingManagers("GetMarketingManaerByEmail", parameters).SingleOrDefault();
        }
        public MarketingManager GetMarketingManagerByID(int ID)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", ID)
                };
                return ReadMarketingManagers("GetMarketingManagerByID", parameters).SingleOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: \n" + e.Message);
                Console.ReadKey();
                return null;
            }
        }
        public List<MarketingManager> GetAllMarketingManagers()
        {
            return ReadMarketingManagers("GetAllMarketingManagers", null);
        }
        public void DeleteMarketingManager(int ID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", ID),
            };
            Write("DeleteMarketingmanager", parameters);
        }
    }
}