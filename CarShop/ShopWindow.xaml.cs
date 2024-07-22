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
    /// Interaction logic for ShopWindow.xaml
    /// </summary>
    public partial class ShopWindow : Window
    {
        private ProductService productService = new();
        public User User { get; set; }

        public ShopWindow()
        {
            InitializeComponent();
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            Product productSelected = CarDataGrid.SelectedItem as Product;

            if (productSelected == null)
            {
                MessageBox.Show("Nothing", "Failed", MessageBoxButton.OK);
                return;
            }

            MessageBox.Show("Buy Successfully", "Successful");

            productSelected.Quantity = productSelected.Quantity - 1;

            productService.UpdateOne(productSelected);

            LoadDataGrid();
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            ProductDetailsWindow main = new();
            main.Show();
        }

        private void BackToMainButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.User = User;
            mainWindow.Show();
            this.Close();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoadDataGrid()
        {
            CarDataGrid.ItemsSource = null; // Xóa grid
            CarDataGrid.ItemsSource = productService.GetAllProducts();
        }

        private void CarDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            WelcomeLabel.Content = "Hello " + User.Name ?? "Bạn";

            LoadDataGrid();

            if (User.Role != "Customer")
            {
                BuyButton.IsEnabled = false;
            }
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


            var result = productService.SearchCarByNameAndQuantity(CarNameTextBox.Text, quan);

            //do kq search vao grid
            CarDataGrid.ItemsSource = null;
            CarDataGrid   .ItemsSource = result;
        }
    }
}