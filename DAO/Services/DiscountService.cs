using DTO.Models;
using DTO.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services
{
	public class DiscountService
	{
		private DiscountRepository _repo = new();
		public List<Discount> GetAllDiscount()
		{
			return _repo.GetAll();
		}
		public void Create02(Product x)
		{
			_repo.Create(x);
		}

		public void Update02(Product x)
		{
			_repo.Update(x);
		}
	}
}
