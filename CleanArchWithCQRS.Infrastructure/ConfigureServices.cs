using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchWithCQRS.Application.Common.Interfaces;
using CleanArchWithCQRS.Domain.Repository;
using CleanArchWithCQRS.Domain.Repository.Command;
using CleanArchWithCQRS.Domain.Repository.Query;
using CleanArchWithCQRS.Infrastructure.Catching;
using CleanArchWithCQRS.Infrastructure.Data;
using CleanArchWithCQRS.Infrastructure.Identity;
using CleanArchWithCQRS.Infrastructure.Repository;
using CleanArchWithCQRS.Infrastructure.Repository.Command;
using CleanArchWithCQRS.Infrastructure.Repository.Query;
using CleanArchWithCQRS.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
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

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders();
			services.Configure<IdentityOptions>(options =>
			{
				// Default Lockout settings.
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = true;
				// Default Password settings.
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = false; // For special character
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;
				// Default SignIn settings.
				options.SignIn.RequireConfirmedEmail = false;
				options.SignIn.RequireConfirmedPhoneNumber = false;
				options.User.RequireUniqueEmail = true;
			});

			services.AddScoped<IIdentityService, IdentityService>();
			services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
			services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));

			services.AddTransient<IProductRepository, ProductRepository>();
			services.AddSingleton<ICacheService, CacheService>();

			//add Redis Cache
			services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = configuration.GetConnectionString("Redis");
			});

			return services;
		}
	}
}
