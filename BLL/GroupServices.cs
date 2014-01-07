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
        //Edit Subscribers in Group
        public void EditGroupSubscribers(GroupFM group)
        {
            GroupDAO dao = new GroupDAO();
            dao.GetSubscribersByGroupID(group.ID);
            dao.EditGroupSubscribers(ConvertGroup(group));
        }
    }
}
