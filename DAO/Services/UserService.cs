using DTO.Models;
using DTO.Repositories;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services
{
    public class UserService
    {
        private UserRepository userRepository = new();

        public User Authenticate(string username, string password)
        {
           var user = userRepository.GetUser(username, password);
           return user;
        }

        public void Register(User user)
        {
            userRepository.AddUser(user);
            return;
        }


    }
}
