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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarShop
{
	/// <summary>
	/// Interaction logic for DetailAccountWindow.xaml
	/// </summary>
	public partial class DetailAccountWindow : Window
	{
		private DAO.Services.UserService _userService = new(); //vai trò chính vì sẽ đưa cuốn sách xuống table

		public User SelectedUser { get; set; } = null;

		public DetailAccountWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			CreateUpdateLabel.Content = "Create a New Account";
			UserIdTextBox.IsEnabled = false;

			if (SelectedUser != null)
			{
				CreateUpdateLabel.Content = "Update a Selected Account Info";

				if(SelectedUser != null)
				{
					UserIdTextBox.Text = SelectedUser.UserId.ToString();
				}
				

				NameTextBox1.Text = SelectedUser.Name;
				UsernameTextBox.Text = SelectedUser.Username;
				PasswordTextBox.Text = SelectedUser.Password;
				EmailTextBox.Text = SelectedUser.Email;
				PhoneTextBox.Text = SelectedUser.Phone;
				AddressTextBox.Text = SelectedUser.Address;
				RollTextBox.Text = SelectedUser.Role;

			}
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{

			User x = new();
			if(SelectedUser != null)
			{
				x.UserId = int.Parse(UserIdTextBox.Text);
			}
			x.Name = NameTextBox1.Text;
			x.Username = UsernameTextBox.Text;
			x.Password = PasswordTextBox.Text;
			x.Email = EmailTextBox.Text;
			x.Phone = PhoneTextBox.Text;
			x.Address = AddressTextBox.Text;
			x.Role = RollTextBox.Text;

			if (SelectedUser == null)
				_userService.CreateUser(x);
			else
				_userService.UpdateUser(x);

			this.Close(); 
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
