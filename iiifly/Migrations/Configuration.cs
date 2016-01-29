using iiifly.Models;

namespace iiifly.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<iiifly.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(iiifly.Models.ApplicationDbContext context)
        {
            string spaceFormat = "https://api-hydra.dlcs.io/customers/{0}/spaces/{1}";
            int spaceSeed = 100;

            //context.SpaceMappings.AddOrUpdate(p => p.SpaceMappingId, 
            //    new SpaceMapping
            //    {
            //        DlcsSpace = string.Format(spaceFormat, SpaceMapping.CustomerId, ++spaceSeed),
            //        SpaceId = spaceSeed,
            //        SpaceMappingId = Guid.NewGuid()
            //    },
            //    new SpaceMapping
            //    {
            //        DlcsSpace = string.Format(spaceFormat, SpaceMapping.CustomerId, ++spaceSeed),
            //        SpaceId = spaceSeed,
            //        SpaceMappingId = Guid.NewGuid()
            //    },
            //    new SpaceMapping
            //    {
            //        DlcsSpace = string.Format(spaceFormat, SpaceMapping.CustomerId, ++spaceSeed),
            //        SpaceId = spaceSeed,
            //        SpaceMappingId = Guid.NewGuid()
            //    });
        }
    }
}
