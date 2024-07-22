using DAO.Services;
using DTO.Models;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private DAO.Services.UserService userService = new();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            var user = userService.Authenticate(username, password);

            if (user == null)
            {
                MessageBox.Show("The account is not exists", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (user.Role)
            {
                case "Customer":
                    // Customer Screen is MainWindow
                    //MainWindow mainWindow = new MainWindow();
                    //mainWindow.User = user;
                    //mainWindow.Show();
                    break;
                case "Staff":
                    // Staff Screen to manage product
                    break;
                case "Admin":
                    // Admin Screen to manage Account
                    break;
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.User = user;
            mainWindow.Show();
            this.Hide();

            // Add your login logic here
            //MessageBox.Show($"Username: {username}\nPassword: {password}");
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Hide();
        }

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}
}
