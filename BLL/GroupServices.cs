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

            //Below is for testing
            GroupVM testGroup1 = new GroupVM();
            testGroup1.GroupName = "Test Group 1";
            groups.Add(testGroup1);
            GroupVM testGroup2 = new GroupVM();
            testGroup2.GroupName = "Test Group 2";
            groups.Add(testGroup2);
            GroupVM testGroup3 = new GroupVM();
            testGroup3.GroupName = "Test Group 3";
            groups.Add(testGroup3);
            GroupVM testGroup4 = new GroupVM();
            testGroup4.GroupName = "Test Group 4";
            groups.Add(testGroup4);
            GroupVM testGroup5 = new GroupVM();
            testGroup5.GroupName = "Test Group 5";
            groups.Add(testGroup5);
            GroupVM testGroup10 = new GroupVM();
            testGroup10.GroupName = "Test Group 10";
            groups.Add(testGroup10);
            GroupVM testGroup20 = new GroupVM();
            testGroup20.GroupName = "Test Group 20";
            groups.Add(testGroup20);
            GroupVM testGroup30 = new GroupVM();
            testGroup30.GroupName = "Test Group 30";
            groups.Add(testGroup30);
            GroupVM testGroup40 = new GroupVM();
            testGroup40.GroupName = "Test Group 40";
            groups.Add(testGroup40);
            GroupVM testGroup50 = new GroupVM();
            testGroup50.GroupName = "Test Group 50";
            groups.Add(testGroup50);
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
