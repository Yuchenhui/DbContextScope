using System.Data.Entity;
using System.Reflection;
using Numero3.EntityFramework.Demo.DomainModel;

namespace Numero3.EntityFramework.Demo.DatabaseContext
{
	public class CommonDbContext : DbContext
	{
		// Map our 'User' model by convention
		public DbSet<User> Users { get; set; }

        public CommonDbContext() : base(new FocusSite().Default().DbConnectMain)
		{}

	    public CommonDbContext(string site) : base(SiteDic.GetFocusSite(site).DbConnectMain)
	    {

	    }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Overrides for the convention-based mappings.
			// We're assuming that all our fluent mappings are declared in this assembly.
			modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(CommonDbContext)));
		}
	}
}
