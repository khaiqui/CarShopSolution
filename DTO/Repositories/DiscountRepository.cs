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

		public void Create(Discount x)
		{
			_context = new();
			_context.Discounts.Add(x);
			_context.SaveChanges();
		}

		public void Update(Discount x)
		{
			_context = new();
			_context.Discounts.Update(x);
			_context.SaveChanges();
		}
	}
}
