using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchWithCQRS.Domain.Repository;
using CleanArchWithCQRS.Infrastructure.Data;
using CleanArchWithCQRS.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchWithCQRS.Infrastructure
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseNpgsql(configuration.GetConnectionString("Tititada") ??
				                  throw new InvalidOperationException("Connection string 'dbcontext not found'")));

			services.AddTransient<IProductRepository, ProductRepository>();
			return services;
		}
	}
}
