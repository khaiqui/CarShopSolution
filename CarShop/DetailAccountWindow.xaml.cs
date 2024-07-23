using DAO.Services;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
	public partial class DetailAccountWindow : Window
	{
		private DAO.Services.UserService _userService = new();

		public User SelectedUser { get; set; } = null;

		public DetailAccountWindow()
		{
			InitializeComponent();
		}

		//Có chỉnh sửa
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			CreateUpdateLabel.Content = "Tạo mới tài khoản";
			UserIdTextBox.IsEnabled = false;

			RoleCombox.Items.Add("Admin");
			RoleCombox.Items.Add("Staff");
			RoleCombox.Items.Add("Customer");

			if (SelectedUser != null)
			{
				CreateUpdateLabel.Content = "Cập nhật thông tin tài khoản đã chọn";

				UserIdTextBox.Text = SelectedUser.UserId.ToString();
				NameTextBox1.Text = SelectedUser.Name;
				UsernameTextBox.Text = SelectedUser.Username;
				PasswordTextBox.Text = SelectedUser.Password;
				EmailTextBox.Text = SelectedUser.Email;
				PhoneTextBox.Text = SelectedUser.Phone;
				AddressTextBox.Text = SelectedUser.Address;
				RoleCombox.Text = SelectedUser.Role;
				RoleCombox.IsEnabled = false;
			}
		}

		//Có chỉnh sửa
		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(NameTextBox1.Text))
			{
				System.Windows.MessageBox.Show("Name không được để trống.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
			{
				System.Windows.MessageBox.Show("Username không được để trống.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (string.IsNullOrWhiteSpace(PasswordTextBox.Text))
			{
				System.Windows.MessageBox.Show("Password không được để trống.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (!IsValidEmail(EmailTextBox.Text))
			{
				System.Windows.MessageBox.Show("Email không hợp lệ.");
				return;
			}

			if (string.IsNullOrWhiteSpace(RoleCombox.Text))
			{
				System.Windows.MessageBox.Show("Role không được để trống.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (_userService.GetAllUsers().Any(u => u.Username == UsernameTextBox.Text && (SelectedUser == null || u.UserId != SelectedUser.UserId)))
			{
				System.Windows.MessageBox.Show("Tên tài khoản đã tồn tại.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (_userService.GetAllUsers().Any(u => u.Phone == PhoneTextBox.Text && (SelectedUser == null || u.UserId != SelectedUser.UserId)))
			{
				System.Windows.MessageBox.Show("Số điện thoại đã tồn tại.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			string phone = PhoneTextBox.Text;
			if (phone.Length < 9 || phone.Length > 12 || !phone.All(char.IsDigit))
			{
				System.Windows.MessageBox.Show("Số điện thoại phải từ 9 đến 12 số và chỉ chứa số.");
				return;
			}

			

			User x = new();
			if(SelectedUser != null)
			{
				x.UserId = int.Parse(UserIdTextBox.Text);
				x.Role = RoleCombox.Text;	//Giữ Role cũ
			}
			else
			{
				x.Role = RoleCombox.SelectedItem != null ? RoleCombox.SelectedItem.ToString() : "1";
			}

			x.Name = NameTextBox1.Text;
			x.Username = UsernameTextBox.Text;
			x.Password = PasswordTextBox.Text;
			x.Email = EmailTextBox.Text;
			x.Phone = PhoneTextBox.Text;
			x.Address = AddressTextBox.Text;
			
			if (SelectedUser == null)
				_userService.CreateUser(x);
			else
				_userService.UpdateUser(x);

			this.Close(); 
		}

		//Close (Có chỉnh sửa)
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		//Xác thực Email
		private bool IsValidEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return false;

			try
			{
				email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
									  RegexOptions.None, TimeSpan.FromMilliseconds(200));

				string DomainMapper(Match match)
				{
					var idn = new IdnMapping();

					string domainName = idn.GetAscii(match.Groups[2].Value);

					return match.Groups[1].Value + domainName;
				}
			}
			catch (RegexMatchTimeoutException e)
			{
				return false;
			}
			catch (ArgumentException e)
			{
				return false;
			}

			try
			{
				return Regex.IsMatch(email,
					@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
					RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
			}
			catch (RegexMatchTimeoutException)
			{
				return false;
			}
		}
	}

}
