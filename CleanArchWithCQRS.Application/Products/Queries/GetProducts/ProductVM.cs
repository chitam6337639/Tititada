using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchWithCQRS.Application.Common.Mappings;
using CleanArchWithCQRS.Domain.Entities;

namespace CleanArchWithCQRS.Application.Products.Queries.GetProducts
{
	public class ProductVM : IMapFrom<Product>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Price { get; set; }
	}
}
