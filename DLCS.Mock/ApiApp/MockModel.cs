using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLCS.Client.Model;

namespace DLCS.Mock.ApiApp
{
    public class MockModel
    {
        public List<Customer> Customers { get; set; }
        public List<PortalUser> PortalUsers { get; set; }
        public List<Space> Spaces { get; set; }
        public List<Queue> Queues { get; set; }

        public static MockModel Build()
        {
            var model = new MockModel
            {
                Customers = CreateCustomers(),
                Queues = CreateQueues(),
                PortalUsers = CreatePortalUsers(),
                Spaces = CreateSpaces(),

            };
            return model;
        }


        private static List<Customer> CreateCustomers()
        {
            return new List<Customer>
            {
                new Customer(1, "admin", "Administrator"),
                new Customer(2, "wellcome", "Wellcome"),
                new Customer(3, "crane", "Crane"),
                new Customer(4, "test", "Testing")
            };
        }

        private static List<Queue> CreateQueues()
        {
            return new List<Queue>
            {
                new Queue(1, 0),
                new Queue(2, 0),
                new Queue(3, 0),
                new Queue(4, 10)
            };
        }

        private static List<PortalUser> CreatePortalUsers()
        {
            return new List<PortalUser>()
            {
                new PortalUser(1, "8b083aee", "adam.christie@digirati.co.uk", new DateTime(2005, 10, 31), new string[0],
                    true),
                new PortalUser(3, "9cee79e8", "tom.crane@digirati.co.uk", new DateTime(2010, 6, 21), new string[0],
                    true),
                new PortalUser(1, "e3afdce8", "admin@dlcs.io", new DateTime(2016, 1, 1), new string[0],
                    true),
                new PortalUser(2, "ef132a3f", "r.kiley@wellcome.ac.uk", new DateTime(1961, 10, 31), new string[0],
                    true)
            };
        }

        private static List<Space> CreateSpaces()
        {
            return new List<Space>();
        }
    }
}
