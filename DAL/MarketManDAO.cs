using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
    public class MarketManDAO
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
        public List<MarketMan> ReadMarketMans(string statement, SqlParameter[] parameters)
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
                    List<MarketMan> subscribers = new List<MarketMan>();
                    while (data.Read())
                    {
                        MarketMan subscriber = new MarketMan();
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
        public void CreateMarketMan(MarketMan user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Active", 1)
            };
            Write("CreateMarketingManager", parameters);
        }
        public MarketMan GetMarketManByEmail(string email)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email)
            };
            return ReadMarketMans("GetMarketingManaerByEmail", parameters).SingleOrDefault();
        }
        public MarketMan GetMarketManByID(int ID)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", ID)
                };
                return ReadMarketMans("GetMarketingManagerByID", parameters).SingleOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: \n" + e.Message);
                Console.ReadKey();
                return null;
            }
        }
        public List<MarketMan> GetAllMarketMans()
        {
            return ReadMarketMans("GetAllMarketingManagers", null);
        }
        public void UpdateMarketMan(MarketMan user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", user.ID),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Password", user.Password)
            };
            Write("UpdateMarketingManager", parameters);
        }
        public void DeleteMarketMan(int ID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", ID),
            };
            Write("DeleteMarketingmanager", parameters);
        }
    }
}