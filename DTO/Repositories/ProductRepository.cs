using DTO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Repositories
{
	public class ProductRepository
	{
		private CarshopDbContext _context;
		public List<Product> GetList()
		{
			_context = new();
			return _context.Products.Include("Discount").Include("Model").ToList();
		}
		public void Create(Product p)
		{
			_context = new();
			_context.Products.Add(p);	
			_context.SaveChanges();
		}
		public void Update(Product p)
		{
			_context = new();
			_context.Products.Update(p);
			_context.SaveChanges();
		}
		public void Delete(Product p)
		{
			_context = new();
			_context.Products.Remove(p);
			_context.SaveChanges();
		}
	}
}
