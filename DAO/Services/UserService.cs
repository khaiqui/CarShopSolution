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
        private UserRepository userRepository = new UserRepository();

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

		public List<User> GetAllUsers()
		{
			return userRepository.GetUser();
		}

		public List<User> SearchAccountByNameAndEmail(string name, string email)
		{
			name = name.ToLower();
			email = email.ToLower();
			return userRepository.GetUser().Where(x => x.Name.ToLower().Contains(name) && x.Email.ToLower().Contains(email)).ToList();

		}

		public void UpdateUser(User x)
		{
			userRepository.Update1(x);
		}

		public void CreateUser(User x)
		{
			userRepository.AddUser(x);
		}

		public void DeleteUser(User x)
		{
			userRepository.Delete(x);
		}

		public List<User> GetAllRoll()
		{
			return userRepository.GetAll();
		}
	}
}
