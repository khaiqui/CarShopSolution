using DTO.Models;
using DTO.Repositories;
using Microsoft.EntityFrameworkCore;
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

		public void Create(Model x)
		{
			_repo.Create(x);
		}

		public void Update(Model x)
		{
			_repo.Update(x);
		}
		public List<Model> SearchByName(string name)
		{
			name = name.ToLower();
			return _repo.GetAll().Where(m => m.ModelName.ToLower().Contains(name)).ToList();
		}
	}
}
