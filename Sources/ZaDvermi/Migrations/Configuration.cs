using System.Collections.Generic;
using ZaDvermi.Models;

namespace ZaDvermi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ZaDvermi.Data.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ZaDvermi.Data.DataContext context)
        {
            // User gorups
            var userGroups = new List<UserGroup>()
                {
                    new UserGroup() {Name = "Administrator", Description = "Administratoøi aplikace"},
                    new UserGroup() {Name = "Manager", Description = "Správce aplikace"},
                    new UserGroup() {Name = "Editor", Description = "Editoøi obsahu"},
                    new UserGroup() {Name = "Sale", Description = "Prodejce, externí spolupracovník"},
                    new UserGroup() {Name = "Standard", Description = "Standardní uživatel"}
                };
            userGroups.ForEach(s => context.UserGroups.Add(s));
            context.SaveChanges();

            // Features
            var features = new List<Feature>
                {
                    new Feature() {FeatureKey = "AA", Description = "mazaní pøíspìvkù na nástìnce"}
                };
            context.SaveChanges();

            // Rights
            var rights = new List<Right>()
                {
                    new Right()
                        {
                            Feature = features.Single(g => g.FeatureKey == "AA"),
                            UserGroup = userGroups.Single(g => g.Name == "Administrator")
                        },
                    new Right()
                        {
                            Feature = features.Single(g => g.FeatureKey == "AA"),
                            UserGroup = userGroups.Single(g => g.Name == "Manager")
                        }


                };
            rights.ForEach(r => context.Rights.Add(r));
            context.SaveChanges();
        }
    }
}
