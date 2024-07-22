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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using Microsoft.Win32;

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
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
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
			this.Close();
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

        private void AddListByFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                ImportProductsFromExcel(filePath);
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                FileName = "ProductsExport.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                ExportProductsToExcel(filePath);
            }
        }

        private void ImportProductsFromExcel(string filePath)
        {
            var existingFile = new FileInfo(filePath);
            using (var package = new ExcelPackage(existingFile))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    Product product = new Product
                    {
                        ProductName = worksheet.Cells[row, 1].Text,
                        Price = decimal.TryParse(worksheet.Cells[row, 2].Text, out decimal price) ? price : (decimal?)null,
                        Image = worksheet.Cells[row, 3].Text,
                        Description = worksheet.Cells[row, 4].Text,
                        ModelId = int.TryParse(worksheet.Cells[row, 5].Text, out int modelId) ? modelId : (int?)null,
                        DiscountId = int.TryParse(worksheet.Cells[row, 6].Text, out int discountId) ? discountId : (int?)null,
                        Quantity = int.TryParse(worksheet.Cells[row, 7].Text, out int quantity) ? quantity : (int?)null
                    };

                    _service.CreateOne(product);
                }
            }

            LoadDataGrid();
            MessageBox.Show("Products imported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExportProductsToExcel(string filePath)
        {
            var products = _service.GetAllProducts();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Products");
                worksheet.Cells[1, 1].Value = "ProductName";
                worksheet.Cells[1, 2].Value = "Price";
                worksheet.Cells[1, 3].Value = "Image";
                worksheet.Cells[1, 4].Value = "Description";
                worksheet.Cells[1, 5].Value = "ModelId";
                worksheet.Cells[1, 6].Value = "DiscountId";
                worksheet.Cells[1, 7].Value = "Quantity";

                int row = 2;
                foreach (var product in products)
                {
                    worksheet.Cells[row, 1].Value = product.ProductName;
                    worksheet.Cells[row, 2].Value = product.Price;
                    worksheet.Cells[row, 3].Value = product.Image;
                    worksheet.Cells[row, 4].Value = product.Description;
                    worksheet.Cells[row, 5].Value = product.ModelId;
                    worksheet.Cells[row, 6].Value = product.DiscountId;
                    worksheet.Cells[row, 7].Value = product.Quantity;
                    row++;
                }

                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);

            }
            MessageBox.Show("Products exported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
