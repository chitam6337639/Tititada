using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchWithCQRS.Infrastructure
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
			IConfiguration configuration)
		{
			return services;
		}
	}
}
