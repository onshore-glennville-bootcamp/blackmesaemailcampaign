using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class GroupServices
    {
        //Gets All Groups and Subscribers for each group
        public List<GroupVM> GetAllGroups()
        {
            GroupDAO dao = new GroupDAO();
            List<GroupVM> groups = new List<GroupVM>();
            foreach (Groups group in dao.GetAllGroups())
            {
                GroupVM vm = ConvertGroup(group);
                groups.Add(vm);
            }
            return groups;
        }
        //Converts Group to VM
        public GroupVM ConvertGroup(Groups group)
        {
            GroupVM vm = new GroupVM();
            GroupDAO dao = new GroupDAO();
            vm.ID = group.ID;
            vm.GroupName = group.GroupName;
            vm.Subscribers = ConvertSubscriberList(dao.GetSubscribersByGroupID(group.ID));
            return vm;
        }
        //Converts From to Group
        public Groups ConvertGroup(GroupFM group)
        {
            Groups vm = new Groups();
            GroupDAO dao = new GroupDAO();
            vm.ID = group.ID;
            vm.GroupName = group.GroupName;
            vm.Subscribers =  ConvertSubscriberList(group.Subscribers);
            return vm;
        }
        //Converts List of Subscribers to VM for GroupVM
        public List<SubscriberVM> ConvertSubscriberList(List<Subscribers> list)
        {
            List<SubscriberVM> vm = new List<SubscriberVM>();
            UserServices log = new UserServices();
            foreach (Subscribers subscriber in list)
            {
                vm.Add(log.ConvertSubscriber(subscriber));
            }
            return vm;
        }
        //Converts List of SubscriberVM to Subscribers for Groups
        public List<Subscribers> ConvertSubscriberList(List<SubscriberVM> list)
        {
            List<Subscribers> subscribers = new List<Subscribers>();
            UserServices log = new UserServices();
            foreach (SubscriberVM subscriber in list)
            {
                subscribers.Add(log.ConvertSubscriber(subscriber));
            }
            return subscribers;
        }
        //Update Subscribers to a Group
        public void UpdateGroupSubscribers(GroupFM group)
        {
            if (group.Subscribers == null)
            {
                group.Subscribers = group.Search;
            }
            GroupDAO dao = new GroupDAO();
            UserServices log = new UserServices();
            List<SubscriberVM> oldSubscribers = GetSubscribersByGroupID(group.ID);
            for (int i = 0; i < oldSubscribers.Count; i++)
            {
                for (int j = 0; j < group.Subscribers.Count; j++)
                {
                    //Compares old group subscribers to new group subscribers and makes changes when they don't match
                    if (oldSubscribers[i].ID == group.Subscribers[j].ID && oldSubscribers[i].EmailList != group.Subscribers[j].EmailList)
                    {
                        if (oldSubscribers[i].EmailList)
                        {
                            dao.DeleteGroupSubscribers(group.ID, oldSubscribers[i].ID);
                        }
                        else
                        {
                            dao.AddGroupSubscribers(group.ID, group.Subscribers[j].ID);
                        }
                    }
                }    
            }
        }
        //Returns list subscriberVM (subscribers in group, if value of emailList = true)
        public List<SubscriberVM> GetSubscribersByGroupID(int groupID)
        {
            GroupDAO dao = new GroupDAO();
            UserServices us = new UserServices();
            List<SubscriberVM> list = us.GetAllSubscribers();
            for (int i = 0; i < dao.GetSubscribersByGroupID(groupID).Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (dao.GetSubscribersByGroupID(groupID)[i].ID == list[j].ID)
                    {
                        list[j].EmailList = true;
                    }
                }
            }
            return list;
        }
        //Returns GroupName by ID
        public string GetGroupNameByID(int groupID)
        {
            foreach (GroupVM vm in GetAllGroups())
            {
                if (vm.ID == groupID)
                {
                    return vm.GroupName;
                }
            }
            return null;
        }
        //Searchs Database for Subscribers and checks the ones currently in list
        public List<SubscriberVM> Search(int groupID, string search)
        {
            UserServices log = new UserServices();
            List<SubscriberVM> results = log.Search(search);
            for (int i = 0; i < GetSubscribersByGroupID(groupID).Count; i++)
            {
                if (GetSubscribersByGroupID(groupID)[i].EmailList)
                {
                    for (int j = 0; j < results.Count; j++)
                    {
                        if (GetSubscribersByGroupID(groupID)[i].ID == results[j].ID)
                        {
                            results[j].EmailList = true;
                        }
                    }
                }
            }
            return results;
        }
    }
}
