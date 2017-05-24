using System;
using System.Linq;

namespace StoreManagement.InfraStructure.Migrations
{
    using System.Data.Entity.Migrations;

    using Context;



    public class Configuration : DbMigrationsConfiguration<MainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(MainContext context)
        {
          
        }
    }
}
