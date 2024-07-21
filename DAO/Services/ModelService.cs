using DTO.Models;
using DTO.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services
{
	public class ModelService
	{
		private ModelRepository _repo = new ModelRepository();
		public List<Model> GetAllModels()
		{
			return _repo.GetAll();
		}

		public void Create01(Product x)
		{
			_repo.Create(x);
		}

		public void Update01(Product x)
		{
			_repo.Update(x);
		}
	}
}
