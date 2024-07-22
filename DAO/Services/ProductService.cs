using DTO.Models;
using DTO.Repositories;
using Microsoft.IdentityModel.Tokens;
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
        public List<Product> SearchCarByNameAndQuantity(string name, int? quan)
        {
            //ko go 2 keyword
            List<Product> result = _repo.GetList();
            if (name.IsNullOrEmpty() && !quan.HasValue)
            {
                return result;
            }
            if (!name.IsNullOrEmpty())
            {
                result = result.Where(x => x.ProductName.ToLower().Contains(name.ToLower())).ToList();
            }
            if (quan.HasValue)
            {
                result = result.Where(x => x.Quantity == quan).ToList();
            }

            return result;
        }

		//Đang bị lỗi logic
		//public List<Product> Search(string name, string desc, int? modelId)
		//{
		//	name = name.ToLower();
		//	desc = desc.ToLower();
		//	return _repo.GetList().Where(
		//		a => a.ProductName.ToLower().Contains(name)
		//		&& a.Description.ToLower().Contains(desc)
		//		&& (modelId != null || a.ModelId == modelId)).ToList();
		//}

	}
		
}
