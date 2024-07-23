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
	/// Interaction logic for AccountManagerWindow.xaml
	/// </summary>
	public partial class AccountManagerWindow : Window
	{
		private DAO.Services.UserService _service = new();

		//public User Account { get; set; }

		public AccountManagerWindow()
		{
			InitializeComponent();
		}

		private void QuitButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}


		private void LoadDateGrid()
		{
			AdminDataGrid.ItemsSource = null; //xóa data
			AdminDataGrid.ItemsSource = _service.GetAllUsers();

		}

		private void AdminDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			string name = NameTextBox.Text.ToLower();
			string email = EmailTextBox.Text.ToLower();

			List<User> result = _service.SearchAccountByNameAndEmail(name, email);
			//F5 Grid
			AdminDataGrid.ItemsSource = null;    //xóa gird
			AdminDataGrid.ItemsSource = result;
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			User selected = AdminDataGrid.SelectedItem as User;

			if (selected == null)
			{
				MessageBox.Show("Vui lòng chọn một tài khoản trước khi xóa", "Select one", MessageBoxButton.OK, MessageBoxImage.Stop);
				return;
			}

			MessageBoxResult answer = MessageBox.Show("Bạn có thực sự muốn xóa tài khoản đã chọn không?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (answer == MessageBoxResult.No)
				return;

			_service.DeleteUser(selected);
			//f5 Gird
			LoadDateGrid();
		}

		private void UpdateButton_Click(object sender, RoutedEventArgs e)
		{
			User selected = AdminDataGrid.SelectedItem as User;

			if (selected == null)
			{
				MessageBox.Show("Vui lòng chọn một tài khoản để nâng cấp", "Select one", MessageBoxButton.OK, MessageBoxImage.Stop);
				return;
			}

			DetailAccountWindow detail = new();
			detail.SelectedUser = selected;
			detail.ShowDialog();
			LoadDateGrid();
		}

		private void CreateButton_Click(object sender, RoutedEventArgs e)
		{
			DetailAccountWindow detail = new();
			detail.ShowDialog();
			LoadDateGrid();
		}

		private void Window_Loaded_1(object sender, RoutedEventArgs e)
		{
			LoadDateGrid();
		}
    }
}
