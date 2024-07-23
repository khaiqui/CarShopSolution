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
	/// Interaction logic for DiscountsManagerWindow.xaml
	/// </summary>
	public partial class DiscountsManagerWindow : Window
	{
		private DiscountService _service = new();
		public DiscountsManagerWindow()
		{
			InitializeComponent();
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			LoadDataGrid();
		}
		private void LoadDataGrid()
		{
			DiscountDataGrid.ItemsSource = null;

			DiscountDataGrid.ItemsSource = _service.GetAllDiscount();

		}

		private void CreateButton_Click(object sender, RoutedEventArgs e)
		{
			DiscountDetailWindow detail = new();
			detail.ShowDialog();
			LoadDataGrid();

		}

		private void UpdateButton_Click(object sender, RoutedEventArgs e)
		{
			Discount selected = DiscountDataGrid.SelectedItem as Discount;
			if (selected == null)
			{
				MessageBox.Show("Please select a discount before updating", "Select one", MessageBoxButton.OK, MessageBoxImage.Stop);
				return;
			}
			DiscountDetailWindow details = new();
			details.Selected = selected;
			details.ShowDialog();

			LoadDataGrid();
		}


		private void QuitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			int? rate = null;
			if(RateTextBox.Text != null)
			{
				rate = int.Parse(RateTextBox.Text);
			}
			var result = _service.SearchByRate(rate);
			DiscountDataGrid.ItemsSource = null;

			DiscountDataGrid.ItemsSource = result;
		}
	}
}
