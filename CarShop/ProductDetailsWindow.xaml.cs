using DAO.Services;
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
    /// Interaction logic for ProductDetailsWindow.xaml
    /// </summary>
        public partial class ProductDetailsWindow : Window       
        {
        private ProductService productService = new();
        public ProductDetailsWindow()
        {
            InitializeComponent();
            string imageUrl = "https://i.imgur.com/mlTNCv3.jpeg";           
            SetImageFromUrl(imageUrl);
        }
        private void SetImageFromUrl(string url)
        {
            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(url, UriKind.Absolute);
                bitmap.EndInit();

                Image.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred when loading image: {ex.Message}");
            }
        }


    }
}
