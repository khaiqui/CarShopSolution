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

			if (SelectedUser != null)
			{
				CreateUpdateLabel.Content = "Update a Selected Account Info";

				UserIdTextBox.Text = SelectedUser.UserId.ToString();
				UserIdTextBox.IsEnabled = false;

				NameTextBox1.Text = SelectedUser.Name;
				UsernameTextBox.Text = SelectedUser.Username;
				PasswordTextBox.Text = SelectedUser.Password;
				EmailTextBox.Text = SelectedUser.Email;
				PhoneTextBox.Text = SelectedUser.Phone;
				AddressTextBox.Text = SelectedUser.Address;
				RollTextBox.Text = SelectedUser.Role;

				//QUAN TRỌNG: ĐỪNG QUÊN JUMP NHẢY ĐẾN ĐÚNG CÁI CATEGORY MÀ CUỐN SÁCH ĐANG THUỘC VÊ
			}
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{

			User x = new();
			x.UserId = int.Parse(UserIdTextBox.Text);
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
