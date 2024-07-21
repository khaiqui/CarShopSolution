using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Repositories
{
    public class DiscountRepository
	{
		private CarshopDbContext _context;
		public List<Discount> GetAll()
		{
			_context = new CarshopDbContext();
			return _context.Discounts.ToList();
		}

		public void Create(Product x)
		{
			_context = new();
			_context.Products.Add(x);
			_context.SaveChanges();
		}

		public void Update(Product x)
		{
			_context = new();
			_context.Products.Update(x);
			_context.SaveChanges();
		}
	}
}
