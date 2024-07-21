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
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
		private ModelService _modelService = new();
		private DiscountService _discountService = new();
		public Product SelectedProduct { get; set; }

		public DetailWindow()
        {
            InitializeComponent();
        }

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ModelIdComboBox.ItemsSource = _modelService.GetAllModels();
			DiscountIdComboBox_Copy.ItemsSource = _discountService.GetAllDiscount();

			ModelIdComboBox.DisplayMemberPath = "ModelName"; //show cột nào
			ModelIdComboBox.SelectedValuePath = "ModelId"; //lấy value nào để dùng
			DiscountIdComboBox_Copy.DisplayMemberPath = "DiscountRate";
			DiscountIdComboBox_Copy.SelectedValuePath = "DiscountId";

			ModelIdComboBox.SelectedValue = 1;
			DiscountIdComboBox_Copy.SelectedValue = 1;

			CreateUpdateLabel.Content = "Create a New Car";


			if (SelectedProduct != null)
			{

				CreateUpdateLabel.Content = "Update a Selected Car Info";

				IDTextBox1.Text = SelectedProduct.ProductId.ToString();
				IDTextBox1.IsEnabled = false;

				NameTextBox1.Text = SelectedProduct.ProductName;
				PriceTextBox1.Text = SelectedProduct.Price.ToString();
				QuantityTextBox1_Copy.Text = SelectedProduct.Quantity.ToString();
				ImageTextBox1_Copy.Text = SelectedProduct.Image.ToString();
				DescriptionTextBox1_Copy1.Text = SelectedProduct.Description;

				ModelIdComboBox.SelectedValue = SelectedProduct.ModelId;
				DiscountIdComboBox_Copy.SelectedValue = SelectedProduct.DiscountId;
			}
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			Product x = new();
			x.ProductId = int.Parse(IDTextBox1.Text);
			x.ProductName = NameTextBox1.Text;
			x.Description = DescriptionTextBox1_Copy1.Text;
			x.Quantity = int.Parse(QuantityTextBox1_Copy.Text);
			x.Price = int.Parse(PriceTextBox1.Text);
			x.Image = ImageTextBox1_Copy.Text;
			x.ModelId = int.Parse(ModelIdComboBox.SelectedValue.ToString());
			x.DiscountId = int.Parse(DiscountIdComboBox_Copy.SelectedValue.ToString());

			if (SelectedProduct == null)
			{
				_modelService.Create01(x);
				_discountService.Create02(x);
			}
			else
			{
				_modelService.Update01(x);
				_discountService.Update02(x);
			}

			this.Close();
		}

		//Close
		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
