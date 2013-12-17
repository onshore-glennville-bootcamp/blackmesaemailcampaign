using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
    class LoginDAO
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
        public List<MarketingManagers> ReadMarketingManagers(string statement, SqlParameter[] parameters)
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
                    List<MarketingManagers> subscribers = new List<MarketingManagers>();
                    while (data.Read())
                    {
                        MarketingManagers subscriber = new MarketingManagers();
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
        public MarketingManagers GetMarketingManagerByID(int ID)
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
    }
}
