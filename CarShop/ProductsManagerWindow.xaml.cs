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
    /// Interaction logic for ProductsManagerWindow.xaml
    /// </summary>
    public partial class ProductsManagerWindow : Window
    {
		private ProductService _service = new();
		private ModelService _modelService = new();
		public ProductsManagerWindow()
        {
            InitializeComponent();
        }
		//HÀM HELPER - CHỈ CẦN PRIVATE
		private void LoadDateGrid()
		{
			ItemsDataGrid.ItemsSource = null; //xóa data
			ItemsDataGrid.ItemsSource = _service.GetAllProducts();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			LoadDateGrid();
			//HelloLabel.Content = "Hello" + Account.FullName;
			//if (Account.Role == 2)
			//{
			//	CreateButton.IsEnabled = false;
			//	UpdateButton.IsEnabled = false;
			//	DeleteButton.IsEnabled = false;
			//}
		}

		private void QuitButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void CreateButton_Click(object sender, RoutedEventArgs e)
		{
			DetailWindow detail = new();
			detail.ShowDialog();
			LoadDateGrid();
		}

		private void UpdateButton_Click(object sender, RoutedEventArgs e)
		{
			Product selected = ItemsDataGrid.SelectedItem as Product;
			if (selected == null)
			{
				MessageBox.Show("Please select a air-conditioner before updating", "Select one", MessageBoxButton.OK, MessageBoxImage.Stop);
				return;
			}
			DetailWindow detail = new();
			detail.SelectedProduct = selected;
			detail.ShowDialog();
			LoadDateGrid();
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			string name = NameTextBox.Text;
			string decsc = DescriptionTextBox.Text;

			ModelIdComboBox.ItemsSource = _modelService.GetAllModels();

			ModelIdComboBox.DisplayMemberPath = "ModelName"; //show cột nào
			ModelIdComboBox.SelectedValuePath = "ModelId"; //lấy value nào để dùng

			int? modelId = null;
			//if (ModelIdComboBox.SelectedValue != null)
			//	modelId = int.Parse(ModelIdComboBox.SelectedValue.ToString());
			List<Product> result = _service.Search(name, decsc, modelId);
			ItemsDataGrid.ItemsSource = null;
			ItemsDataGrid.ItemsSource = result;
		}
	}
}
