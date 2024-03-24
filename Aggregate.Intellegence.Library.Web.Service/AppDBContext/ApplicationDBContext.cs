using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Aggregate.Intellegence.Library.Web.Service.AppDBContext;
using Aggregate.Intellegence.Library.Web.Service.AppDBContext.Configuration;
using Aggregate.Intellegence.Library.Web.Service.Models;


namespace Aggregate.Intellegence.Library.Web.Service.AppDBContext
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)  : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
        public DbSet<Book> books { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<User> users { get; set; }
    }
}
