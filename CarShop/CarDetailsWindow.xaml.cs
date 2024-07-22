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
    /// Interaction logic for CarDetailsWindow.xaml
    /// </summary>
    public partial class CarDetailsWindow : Window
    {
        private ProductService _service = new();
        private ModelService _modelService = new();
        private DiscountService _discountService = new();
        public Product SelectedCar { get; set; } = null;
        public CarDetailsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ModelComboBox.ItemsSource = _modelService.GetAllModels();
            DiscountComboBox.ItemsSource = _discountService.GetAllDiscount();

            ModelComboBox.DisplayMemberPath = "ModelName";
            ModelComboBox.SelectedValuePath = "ModelId";

            DiscountComboBox.DisplayMemberPath = "DiscountRate";
            DiscountComboBox.SelectedValuePath = "DiscountId";

            ProductIdTextBox.IsEnabled = false;
            CarModelLabel.Content = "Create a new car";
            if (SelectedCar != null)
            {
                CarModelLabel.Content = "Update a selected car infor";
                ProductIdTextBox.Text = SelectedCar.ProductId.ToString();
                ProductNameTextBox.Text = SelectedCar.ProductName.ToString();
                PriceTextBox.Text = SelectedCar.Price.ToString();
                DescriptionTexBox.Text = SelectedCar.ToString();
                QuantityTextBox.Text = SelectedCar.Quantity.ToString();

                ModelComboBox.SelectedValue = SelectedCar.ModelId;
                DiscountComboBox.SelectedValue = SelectedCar.DiscountId;
               
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Product x = new Product();
          
            x.ProductId = int.Parse(ProductIdTextBox.Text);
            x.ProductName = ProductNameTextBox.Text;
            x.Price = decimal.Parse(PriceTextBox.Text);
            
            x.Description = DescriptionTexBox.Text;


            x.Quantity = int.Parse(QuantityTextBox.Text);

            x.ModelId = int.Parse(ModelComboBox.SelectedValue.ToString());
            x.DiscountId = int.Parse(DiscountComboBox.SelectedValue.ToString());



            if (SelectedCar == null)
            {
                _service.CreateOne(x);
            }
            else
            {
                _service.UpdateOne(x);
            }

            this.Close();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
