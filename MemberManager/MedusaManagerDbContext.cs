using System.Data.Entity;
using Medusa.Business.Entities;
using Medusa.Business.Migrations;

namespace Medusa.Business
{

    public class MedusaManagerDbContext : DbContext
    {
        //public static string ConnString { get; set; }

        //public MedusaManagerDbContext() : base(ConnString)

        //public static string ConnString =
        //    "Server=10.16.38.129;Database=Phuong;User Id=sa;Password=admin@123;MultipleActiveResultSets=true;";

        public MedusaManagerDbContext() : base("Medusa")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MedusaManagerDbContext, Configuration>());
        }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberSkill> MemberSkills { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Server> Servers { get; set; }
    }
}
