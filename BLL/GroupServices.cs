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
                vm.Subscribers = ConvertSubscribers(dao.GetSubscribersByGroupID(group.ID));
                groups.Add(vm);
            }
            return groups;
        }
        //Converts Group to VM
        public GroupVM ConvertGroup(Groups group)
        {
            GroupVM vm = new GroupVM();
            vm.ID = group.ID;
            vm.GroupName = group.GroupName;
            return vm;
        }
        //Converts List of Subscribers to VM for GroupVM
        public SubscribersVM ConvertSubscribers(List<Subscribers> list)
        {
            SubscribersVM vm = new SubscribersVM();
            UserServices log = new UserServices();
            foreach (Subscribers subscriber in list)
            {
                vm.Subscribers.Add(log.ConvertSubscriber(subscriber));
            }
            return vm;
        }
        public void CreateGroup(GroupFM groups)
        {
        //Create a new Subscriber group
            GroupDAO newGroup = new GroupDAO();
            newGroup.CreateGroup(groups.GroupName);
        }
    }
}
