﻿using DTO.Models;
using DTO.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services
{
	public class ProductService
	{
		private ProductRepository _repo = new();
		public List<Product> GetAllProducts()
		{
			return _repo.GetList();
		} 
		public void CreateOne(Product product)
		{
			_repo.Create(product);
		}
		public void UpdateOne(Product product)
		{
			_repo.Update(product);
		}
		public void DeleteOne(Product product)
		{
			_repo.Delete(product);
		}

		//search?
	}
}
