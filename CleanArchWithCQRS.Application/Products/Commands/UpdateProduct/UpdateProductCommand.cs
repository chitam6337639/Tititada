﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CleanArchWithCQRS.Application.Products.Commands.UpdateProduct
{
	public class UpdateProductCommand : IRequest<int>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Price { get; set; }
	}
}
