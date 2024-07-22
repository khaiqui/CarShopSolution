using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Repositories
{
    public class ModelRepository
	{
		private CarshopDbContext _context;
		public List<Model> GetAll()
		{
			_context = new CarshopDbContext();
			return _context.Models.ToList();
		}

		public void Create(Model x)
		{
			_context = new();
			_context.Models.Add(x);
			_context.SaveChanges();
		}

		public void Update(Model x)
		{
			_context = new();
			_context.Models.Update(x);
			_context.SaveChanges();
		}
	}
}
