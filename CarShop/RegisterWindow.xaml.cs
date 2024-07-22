using DTO.Models;
using DAO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarShop
{
    /// <summary>
    /// Interaction logic for RegisterWindo.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private DAO.Services.UserService  userService = new();
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User
            {
                Name = nameTextBox.Text,
                Email = emailTextBox.Text,
                Address = addressTextBox.Text,
                Phone = phoneTextBox.Text,
                Username = usernameTextBox.Text,
                Password = passwordBox.Password,
                Role = "Customer" // Default role
            };

            if (IsValidUser(newUser))
            {
                MessageBox.Show("Registration successful!", "Register Successfully", MessageBoxButton.OK);
                userService.Register(newUser);
                this.Close();

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
            }
            else
            {
                MessageBox.Show("Please fill in all required fields.");
            }
        }

        private bool IsValidUser(User user)
        {
            return !string.IsNullOrEmpty(user.Name) &&
                   !string.IsNullOrEmpty(user.Email) &&
                   !string.IsNullOrEmpty(user.Address) &&
                   !string.IsNullOrEmpty(user.Phone) &&
                   !string.IsNullOrEmpty(user.Username) &&
                   !string.IsNullOrEmpty(user.Password);
        }

        private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
