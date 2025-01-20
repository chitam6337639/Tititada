using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchWithCQRS.Domain.Entities;
using CleanArchWithCQRS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchWithCQRS.Infrastructure.Data
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
		{

		}
		public DbSet<Product> Products { get; set; }

	}
}
