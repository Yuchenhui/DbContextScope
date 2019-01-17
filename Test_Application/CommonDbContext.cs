using System.Data.Entity;
using System.Reflection;
using Mehdime.Entity;

namespace Test_Application
{
	public class CommonDbContext : DbContext
	{
		// Map our 'User' model by convention
		public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public CommonDbContext() : base()
		{}

	    public CommonDbContext(string connect) : base(connect)
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
