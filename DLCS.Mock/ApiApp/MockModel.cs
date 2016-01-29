using System;
using System.Collections.Generic;
using System.Linq;
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
            var images = CreateImages(spaces, model.SpaceDefaultRoles, model.ImageRoles);
            model.Images = images;
            model.BatchImages = new Dictionary<string, List<string>>();
            model.Batches = CreateBatches(images, model.BatchImages);
            RecalculateCounters(model);
            return model;
        }

        private static List<Batch> CreateBatches(List<Image> images, Dictionary<string, List<string>> batchImages)
        {
            var r = new Random();
            var batches = new List<Batch>();
            int batchId = 100001;
            Batch currentBatch = null;
            int batchSize = -1;
            int counter = -1;
            int currentCustomer = -1;
            List<string> imagesInBatch = null;
            foreach (var image in images)
            {
                if (counter++ > batchSize || image.CustomerId != currentCustomer)
                {
                    // save the old batch
                    if (currentBatch != null)
                    {
                        batches.Add(currentBatch);
                        batchImages.Add(currentBatch.Id, imagesInBatch);
                    }
                    // start a new batch
                    currentCustomer = image.CustomerId;
                    counter = 1;
                    batchSize = r.Next(3, 10);
                    currentBatch = new Batch(batchId++.ToString(), image.CustomerId, image.Created.AddSeconds(-1));
                    imagesInBatch = new List<string>();
                }
                imagesInBatch.Add(image.Id);
            }
            batches.Add(currentBatch);
            batchImages.Add(currentBatch.Id, imagesInBatch);
            return batches;
        }
        
        private static List<Image> CreateImages(List<Space> spaces, Dictionary<string, List<string>> spaceDefaultRoles, Dictionary<string, List<string>> imageRoles)
        {
            var images = new List<Image>();
            foreach (var space in spaces)
            {
                images.AddRange(MakeImagesForSpace(20, space, spaceDefaultRoles, imageRoles));
            }
            return images;
        }

        private static List<Image> MakeImagesForSpace(int howMany, Space space, Dictionary<string, List<string>> spaceDefaultRoles, Dictionary<string, List<string>> imageRoles)
        {
            Random r = new Random();
            var images = new List<Image>();
            var ongoing = space.ModelId%2 == 0;
            var queued = ongoing ? DateTime.Now.AddHours(-4) : new DateTime(2015, 11, 30); 
            for (int i = 0; i < howMany; i++)
            {
                DateTime? dequeued = ongoing ? (DateTime?) null : queued.AddHours(1).AddSeconds(i * 5);
                if (ongoing && i < 6) dequeued = DateTime.Now.AddSeconds(-80 + 9*i);
                DateTime? finished = ongoing ? (DateTime?)null : queued.AddSeconds(3608).AddSeconds(i * 7);
                if (ongoing && i < 4) finished = DateTime.Now.AddSeconds(-60 + 9 * i);
                var id = Guid.NewGuid().ToString().Substring(0, 8) + i.ToString().PadLeft(5, '0');
                var image = new Image(space.CustomerId, space.ModelId, id,
                    DateTime.Now, "https://customer.com/images/" + id + ".tiff", null,
                    r.Next(2000,11000), r.Next(3000,11000), space.DefaultMaxUnauthorised,
                    queued, dequeued, finished, !finished.HasValue, null,
                    space.DefaultTags, "b12345678", null, null, i, 0, 0);
                images.Add(image);
                if (spaceDefaultRoles.ContainsKey(space.Id))
                {
                    var roles = spaceDefaultRoles[space.Id];
                    if (roles.Any())
                    {
                        imageRoles.Add(image.Id, roles);
                    }
                }
            }
            return images;
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
                new Space(11, test, "test1", DateTime.Now, null, 400),
                new Space(12, test, "test2", DateTime.Now, new [] {"tag1", "tag2"}, -1)
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
            model.SetBatchCounts();
            model.SetQueueSizes();
        }

        private void SetBatchCounts()
        {
            foreach (var batch in Batches)
            {
                var imageIds = BatchImages[batch.Id];
                var images = Images.Where(i => imageIds.Contains(i.Id)).ToList();
                batch.Count = images.Count;
                batch.Completed = images.Count(i => i.Finished.HasValue);
                if (images.All(i => i.Finished.HasValue))
                {
                    batch.Finished = images.Select(i => i.Finished).Max();
                }
                else
                {
                    batch.EstCompletion = DateTime.Now.AddMinutes(3);
                }
            }
        }

        private void SetQueueSizes()
        {
            var totalByCustomer = new Dictionary<int, int>();
            foreach (var image in Images)
            {
                if (!totalByCustomer.ContainsKey(image.CustomerId))
                {
                    totalByCustomer[image.CustomerId] = 0;
                }
                if (!image.Finished.HasValue)
                {
                    totalByCustomer[image.CustomerId] += 1;
                }
            }
            foreach (var queue in Queues)
            {
                if (totalByCustomer.ContainsKey(queue.ModelId))
                {
                    queue.Size = totalByCustomer[queue.ModelId];
                }
            }
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
    }
}
