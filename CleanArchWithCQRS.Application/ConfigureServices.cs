﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CleanArchWithCQRS.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchWithCQRS.Application
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddMediatR(ctg =>
			{
				ctg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				ctg.AddBehavior(typeof(IPipelineBehavior<,>) ,typeof(ValidationBehaviour<,>));
			});
			return services;
		}
	}
}
