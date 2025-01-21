using CleanArchWithCQRS.Domain.Repository.Query;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchWithCQRS.Infrastructure.Repository.Query
{
	public class QueryRepository<T> : IQueryRepository<T> where T : class
	{
		
	}
}
