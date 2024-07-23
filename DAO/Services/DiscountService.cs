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
		public void Create(Discount x)
		{
			_repo.Create(x);
		}

		public void Update(Discount x)
		{
			_repo.Update(x);
		}
		public List<Discount> SearchByRate(int? rate)
		{	
			if (rate != null )
			{
				return _repo.GetAll().Where(m => m.DiscountRate == rate).ToList();
			}
			return _repo.GetAll();
		}
	}
}
