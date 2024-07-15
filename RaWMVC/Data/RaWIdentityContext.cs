using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Areas.Identity.Data;

namespace RaWMVC.Data;

public class RaWIdentityContext : IdentityDbContext<RaWMVCUser>
{
	public RaWIdentityContext(DbContextOptions<RaWIdentityContext> options)
		: base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var connectionString = "Server=LAPTOP-M2M74TDG\\SQLEXPRESS;Database=RaWIdentityContext;User Id=itsjuneka; password=Quynhnhu@14; TrustServerCertificate=True; Trusted_Connection=False; MultipleActiveResultSets=true;";
		optionsBuilder.UseSqlServer(connectionString);

	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		// Customize the ASP.NET Identity model and override the defaults if needed.
		// For example, you can rename the ASP.NET Identity table names and more.
		// Add your customizations after calling base.OnModelCreating(builder);
	}
}
