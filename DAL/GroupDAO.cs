﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
    public class GroupDAO
    {
        //Writes to database
        public void Write(string statement, SqlParameter[] parameters)
        {
            SubscriberDAO dao = new SubscriberDAO();
            dao.Write(statement, parameters);
        }
        //Read Subscribers table of database
        public List<Groups> ReadGroups(string statement, SqlParameter[] parameters)
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
                    List<Groups> groups = new List<Groups>();
                    while (data.Read())
                    {
                        Groups group = new Groups();
                        group.ID = Convert.ToInt32(data["ID"]);
                        group.GroupName = data["Name"].ToString();
                        groups.Add(group);
                    }
                    try
                    {
                        return groups;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }
        //Gets a list of all Groups in db
        public List<Groups> GetAllGroups()
        {
            return ReadGroups("GetAllGroups", null);
        }
        //List of Subscribers by GroupID
        public List<Subscribers> GetSubscribersByGroupID(int groupID)
        {
            SubscriberDAO dao = new SubscriberDAO();
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@GroupID", groupID)
            };
            return dao.ReadSubscribers("GetGroupSubscribers", parameters);
        }

        public void CreateGroup(string GroupName)
        {
            
        }
        public void AddGroupSubscribers(int groupID, int subscriberID)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@G_ID", groupID),
                new SqlParameter("@S_ID", subscriberID)
            };
            Write("AddSubscriberToGroup", parameters);
        }
        //Deactivates Subscribers in a Group
        public void DeleteGroupSubscribers(int groupID, int subscriberID)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@G_ID", groupID),
                new SqlParameter("@S_ID", subscriberID)
            };
            Write("DeleteSubscriberFromGroup", parameters);
        }
    }
}
