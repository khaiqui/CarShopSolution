using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Repositories
{
    public class UserRepository
    {
        private CarshopDbContext _context;

        public User GetUser(string username, string password)
        {
            _context = new CarshopDbContext();
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            return user;
        }

        public void AddUser(User user)
        {
            _context = new ();
            _context.Users.Add(user);
            _context.SaveChanges();
            return;
        }

		public List<User> GetUser()
		{
			_context = new CarshopDbContext();

			return _context.Users.ToList();
		}

		public void Update1(User x)
		{
			_context = new();
			_context.Users.Update(x);
			_context.SaveChanges();
		}

		public void Delete(User x)
		{
			_context = new();
			_context.Users.Remove(x);
			_context.SaveChanges();
		}

		public List<User> GetAll()
		{
			_context = new CarshopDbContext();
			return _context.Users.ToList();
		}
	}
}
