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
    /// Interaction logic for ProductsManagerWindow.xaml
    /// </summary>
    public partial class ProductsManagerWindow : Window
    {
		private ProductService _service = new();
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
		//==================================================================

	}
}
