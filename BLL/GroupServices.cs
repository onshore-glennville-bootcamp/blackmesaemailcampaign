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
        public List<GroupVM> GetAllGroups()
        {
            //SubscriberGroupsDAO dao = new SubscriberGroupsDAO();
            List<GroupVM> groups = new List<GroupVM>();
            //foreach (Groups group in dao.GetAllGroups())
            //{
            //    GroupVM vm = ConvertGroup(group);
            //    vm.Subscribers = dao.GetSubscribersByGroupID(group.ID);
            //    groups.Add(vm);
            //}
            return groups;
        }
        public GroupVM ConvertGroup(Groups group)
        {
            GroupVM vm = new GroupVM();
            vm.ID = group.ID;
            vm.GroupName = group.GroupName;
            return vm;
        }
    }
}
