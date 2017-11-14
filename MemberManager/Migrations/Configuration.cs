using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Medusa.Business.Entities;

namespace Medusa.Business.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MedusaManagerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MedusaManagerDbContext context)
        {
            MedusaManagerDbContext ctx = new MedusaManagerDbContext();
            //Server s = new Server();
            //s.Id = 1;
            //s.Ip = "127.0.0.1";

            //Project p = new Project();
            //p.Id = 1;
            //p.Name = "Slogan";
            //p.StartDate = DateTime.Now;
            //p.EndDate = DateTime.Now;
            //s.Projects.Add(p);
            //ctx.Servers.AddOrUpdate(s);
            //ctx.Projects.AddOrUpdate(p);
            ctx.SaveChanges();
        }
    }
}
