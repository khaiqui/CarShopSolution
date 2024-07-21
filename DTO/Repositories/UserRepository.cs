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
            _context = new CarshopDbContext();
            _context.Users.Add(user);
            _context.SaveChanges();
            return;
        }
    }
}
