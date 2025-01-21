using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchWithCQRS.Domain.Repository.Command;
using CleanArchWithCQRS.Infrastructure.Data;

namespace CleanArchWithCQRS.Infrastructure.Repository.Command
{
	public class CommandRepository<T> : ICommandRepository<T> where T : class
	{
		protected readonly AppDbContext _context;

		public CommandRepository(AppDbContext context)
		{
			_context = context;
		}

		// Insert
		public async Task<T> AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		// Update
		public async Task UpdateAsync(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		// Delete
		public async Task DeleteAsync(T entity)
		{
			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
		}
	}
}
