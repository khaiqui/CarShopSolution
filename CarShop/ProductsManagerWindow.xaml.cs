using DAO.Services;
using DTO.Models;
using Microsoft.IdentityModel.Tokens;
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
	/// Interaction logic for ProductsManagerWindow.xaml
	/// </summary>
	public partial class ProductsManagerWindow : Window
	{

		private ProductService _service = new();
		public ProductsManagerWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			LoadDataGrid();
		}

		private void LoadDataGrid()
		{
			ProductDataGrid.ItemsSource = null;

			ProductDataGrid.ItemsSource = _service.GetAllProducts();

		}

		private void CreateButton_Click(object sender, RoutedEventArgs e)
		{
			CarDetailsWindow detail = new();
			//render
			detail.ShowDialog();
			//F crid
			LoadDataGrid();
		}

		private void UpdateButton_Click(object sender, RoutedEventArgs e)
		{
			Product selected = ProductDataGrid.SelectedItem as Product;
			if (selected == null)
			{
				MessageBox.Show("Please select a car before updating", "Select one", MessageBoxButton.OK, MessageBoxImage.Stop);
				return;
			}
			CarDetailsWindow details = new();
			details.SelectedCar = selected;
			details.ShowDialog();

			LoadDataGrid();
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			Product selected = ProductDataGrid.SelectedItem as Product;

			if (selected == null)
			{
				MessageBox.Show("Please select a car before deleting", "Select one", MessageBoxButton.OK, MessageBoxImage.Stop);
				return;
			}
			MessageBoxResult answer = MessageBox.Show("Do you really want to delete this car?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (answer == MessageBoxResult.No) return;

			_service.DeleteOne(selected);

			LoadDataGrid();
		}

		private void QuitButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			int? quan = null;
			int tmpQuan;
			bool quanStatus = int.TryParse(QuantityTextBox.Text, out tmpQuan);
			if (!quanStatus && !QuantityTextBox.Text.IsNullOrEmpty())
			{
				MessageBox.Show("Incorrect quantity. Please type an integer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			else if (quanStatus == true)
			{
				quan = tmpQuan;
			}


			var result = _service.SearchCarByNameAndQuantity(CarNameTextBox.Text, quan);

			//do kq search vao grid
			ProductDataGrid.ItemsSource = null;
			ProductDataGrid.ItemsSource = result;
		}
	}
}
