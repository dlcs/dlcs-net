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
        // Entities
        public List<Customer> Customers { get; set; }
        public List<PortalUser> PortalUsers { get; set; }
        public List<NamedQuery> NamedQueries { get; set; }
        public List<OriginStrategy> OriginStrategies { get; set; }
        public List<AuthService> AuthServices { get; set; }
        public List<RoleProvider> RoleProviders { get; set; }
        public List<Role> Roles { get; set; }
        public List<Space> Spaces { get; set; }
        public List<Queue> Queues { get; set; }
        public List<Image> Images { get; set; }  
        public List<Batch> Batches { get; set; }

        // collections that can't be generated from entities alone
        public Dictionary<string, List<string>> AuthServiceParentChild { get; set; } 
        public Dictionary<string, string> RoleAuthService { get; set; } 
        public Dictionary<string, List<string>> SpaceDefaultRoles { get; set; }
        public Dictionary<string, List<string>> ImageRoles { get; set; }
        public Dictionary<string, List<string>> BatchImages { get; set; } 



        public static MockModel Build()
        {
            var model = new MockModel();
            var customers = CreateCustomers();
            model.Customers = customers;
            model.PortalUsers = CreatePortalUsers(customers);
            model.NamedQueries = CreateNamedQueries(customers);
            model.OriginStrategies = CreateOriginStrategies(customers);
            model.AuthServiceParentChild = new Dictionary<string, List<string>>();
            var authServices = CreateAuthServices(customers, model.AuthServiceParentChild);
            model.AuthServices = authServices;
            model.RoleProviders = CreateRoleProviders(authServices);
            model.RoleAuthService = new Dictionary<string, string>();
            var roles = CreateRoles(customers, authServices, model.RoleAuthService);
            model.Roles = roles;
            model.SpaceDefaultRoles = new Dictionary<string, List<string>>();
            var spaces = CreateSpaces(customers, roles, model.SpaceDefaultRoles);
            model.Spaces = spaces;
            model.Queues = CreateQueues(customers);
            model.ImageRoles = new Dictionary<string, List<string>>();
            var images = CreateImages(spaces, model.ImageRoles);
            model.Images = images;
            model.BatchImages = new Dictionary<string, List<string>>();
            model.Batches = CreateBatches(customers, model.BatchImages);
            RecalculateCounters(model);
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

        private static List<PortalUser> CreatePortalUsers(List<Customer> customers)
        {
            return new List<PortalUser>
            {
                new PortalUser(customers.GetByName("admin").ModelId, 
                    "8b083aee", "adam.christie@digirati.co.uk", new DateTime(2005, 10, 31), new string[0], true),
                new PortalUser(customers.GetByName("admin").ModelId,
                    "e3afdce8", "admin@dlcs.io", new DateTime(2016, 1, 1), new string[0], true),
                new PortalUser(customers.GetByName("wellcome").ModelId,
                    "ef132a3f", "r.kiley@wellcome.ac.uk", new DateTime(1961, 10, 31), new string[0], true),
                new PortalUser(customers.GetByName("test").ModelId,
                    "9cee79e8", "tom.crane@digirati.co.uk", new DateTime(2010, 6, 21), new string[0], true)
            };
        }

        private static List<NamedQuery> CreateNamedQueries(List<Customer> customers)
        {
            return new List<NamedQuery>
            {
                new NamedQuery(customers.GetByName("test").ModelId, "bob", "{ \"data\": \"data\" }"),
                new NamedQuery(customers.GetByName("test").ModelId, "manifest", "{ \"data\": \"data\" }"),
            };
        }


        private static List<OriginStrategy> CreateOriginStrategies(List<Customer> customers)
        {
            return new List<OriginStrategy>
            {
                new OriginStrategy(customers.GetByName("wellcome").ModelId, 
                    "basic", "https://wellcomelibrary.org/service/asset(.+)", "https", "s3://wellcome/path-to-origin-creds"),
                new OriginStrategy(customers.GetByName("test").ModelId,
                    "basic", "https://example.org/images/(.+)", "https", "s3://test/path-to-origin-creds"),
                new OriginStrategy(customers.GetByName("test").ModelId,
                    "ftps", "ftps://example.org/images/(.+)", "ftps", "s3://test/path-to-ftp-creds")
            };
        }
        
        private static List<AuthService> CreateAuthServices(List<Customer> customers, Dictionary<string, List<string>> authServiceParentChild)
        {
            int wellcome = customers.GetByName("wellcome").ModelId;
            int test = customers.GetByName("test").ModelId;
            var authServices = new List<AuthService>
            {
                new AuthService(wellcome, "wellcome-clickthrough-login", "clickthrough", "http://iiif.io/api/auth/0/login", 0,
                    "Terms and Conditions", "<p>clickthrough...</p>", 
                    "Terms and Conditions", "<p>More detailed info</p>", "Accept terms"),
                new AuthService(wellcome, "wellcome-clickthrough-token", "clickthrough-token", "http://iiif.io/api/auth/0/token", 1800,
                    "token service", null, null, null, null),
                new AuthService(wellcome, "wellcome-clickthrough-logout", "clickthrough-logout", "http://iiif.io/api/auth/0/logout", 0,
                    "Forget terms", null, null, null, null),

                new AuthService(wellcome, "wellcome-delegated-login", "delegated-login", "http://iiif.io/api/auth/0/login", 0,
                    "Log in to view protected material", "<p>More detailed text for login prompt in UV</p>", 
                    null, null, "Log in"),
                new AuthService(wellcome, "wellcome-delegated-token", "delegated-token", "http://iiif.io/api/auth/0/token", 1800,
                    "token service", null, null, null, null),
                new AuthService(wellcome, "wellcome-delegated-logout", "delegated-logout", "http://iiif.io/api/auth/0/logout", 0,
                    "Log out", null, null, null, null),

                new AuthService(test, "test-clickthrough-login", "clickthrough", "http://iiif.io/api/auth/0/login", 0,
                    "Terms and Conditions", "<p>clickthrough...</p>",
                    "Terms and Conditions", "<p>More detailed info</p>", "Accept terms"),
                new AuthService(test, "test-clickthrough-token", "clickthrough-token", "http://iiif.io/api/auth/0/token", 1800,
                    "token service", null, null, null, null),
                new AuthService(test, "test-clickthrough-logout", "clickthrough-logout", "http://iiif.io/api/auth/0/logout", 0,
                    "Forget terms", null, null, null, null),
            };

            authServiceParentChild.Add(
                    authServices.GetByIdPart("wellcome-clickthrough-login").Id,
                        new List<string> {  authServices.GetByIdPart("wellcome-clickthrough-token").Id,
                                            authServices.GetByIdPart("wellcome-clickthrough-logout").Id });

            authServiceParentChild.Add(
                    authServices.GetByIdPart("wellcome-delegated-login").Id,
                        new List<string> {  authServices.GetByIdPart("wellcome-delegated-token").Id,
                                            authServices.GetByIdPart("wellcome-delegated-logout").Id });

            authServiceParentChild.Add(
                    authServices.GetByIdPart("test-clickthrough-login").Id,
                        new List<string> {  authServices.GetByIdPart("test-clickthrough-token").Id,
                                            authServices.GetByIdPart("test-clickthrough-logout").Id });

            return authServices;
        }


        private static List<RoleProvider> CreateRoleProviders(List<AuthService> authServices)
        {
            var wellcomeDelegated = authServices.GetByIdPart("wellcome-delegated-login");
            return new List<RoleProvider>
            {
                new RoleProvider(wellcomeDelegated.CustomerId, wellcomeDelegated.ModelId, "{ Some CAS or OAuth details }", 
                    "s3://wellcome/path-to-sso-backchannel-creds-if-required")
            };
        }

        private static List<Role> CreateRoles(List<Customer> customers, List<AuthService> authServices, Dictionary<string, string> roleAuthService)
        {
            int wellcome = customers.GetByName("wellcome").ModelId;
            int test = customers.GetByName("test").ModelId;
            
            var roles = new List<Role>
            {
                new Role(wellcome, "clickthrough", "Click through",
                    "Role for DLCS-enforced auth with no delegation", new [] { "Requires Registration", "reqreg"}),
                new Role(wellcome, "clinical", "Clinical Delegate to wellcomelibrary.org",
                    "Role for DLCS-enforced auth with delegation to customer", new [] { "Clinical Images", "Healthcare professional" }),
                new Role(wellcome, "staff", "Staff Delegate to wellcomelibrary.org",
                    "Role for DLCS-enforced auth with delegation to customer", new [] { "Wellcome Staff Member" }),
                new Role(wellcome, "restricted", "Restricted Delegate to wellcomelibrary.org",
                    "Role for DLCS-enforced auth with delegation to customer", new [] { "restricted" }),

                new Role(test, "clickthrough", "Click through",
                    "Role for DLCS-enforced auth with no delegation", new [] { "acceptterms" }),
            };

            roleAuthService.Add(roles[0].Id, authServices.GetByIdPart("wellcome-clickthrough-login").Id);
            roleAuthService.Add(roles[1].Id, authServices.GetByIdPart("wellcome-delegated-login").Id);
            roleAuthService.Add(roles[2].Id, authServices.GetByIdPart("wellcome-delegated-login").Id);
            roleAuthService.Add(roles[3].Id, authServices.GetByIdPart("wellcome-delegated-login").Id);
            roleAuthService.Add(roles[4].Id, authServices.GetByIdPart("test-clickthrough-login").Id);
            return roles;
        }


        private static List<Space> CreateSpaces(List<Customer> customers, List<Role> roles, Dictionary<string, List<string>> spaceDefaultRoles)
        {
            int wellcome = customers.GetByName("wellcome").ModelId;
            int test = customers.GetByName("test").ModelId;
            var spaces = new List<Space>
            {
                new Space(1, wellcome, "wellcome1", DateTime.Now, null, -1),
                new Space(2, wellcome, "wellcome2", DateTime.Now, null, -1),
                new Space(11, test, "test1", DateTime.Now, null, -1),
                new Space(21, test, "test2", DateTime.Now, null, -1)
            };

             
            spaceDefaultRoles.Add(
                    spaces[1].Id,
                        new List<string> { roles.GetByCustAndId(wellcome, "clinical").Id });
            spaceDefaultRoles.Add(
                    spaces[3].Id,
                        new List<string> { roles.GetByCustAndId(test, "clickthrough").Id });

            return spaces;
        }

        public static void RecalculateCounters(MockModel model)
        {
            //model.SetBatchCounts();
            //model.SetQueueSize();
        }


        private static List<Queue> CreateQueues(List<Customer> customers)
        {
            return new List<Queue>
            {
                new Queue(1),
                new Queue(2),
                new Queue(3),
                new Queue(4)
            };
        }

        private static List<Batch> CreateBatches()
        {
            return new List<Batch>();
        }

        
    }
}
