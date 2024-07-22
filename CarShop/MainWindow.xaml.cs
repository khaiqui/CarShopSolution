using DTO.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User User { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

		private void ManageModelsButton_Click(object sender, RoutedEventArgs e)
		{

		}

        private void ManageProductsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BuyProductsButton_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoToProductsButton_Click(object sender, RoutedEventArgs e)
        {
            ShopWindow shopWindow = new ShopWindow();
            shopWindow.User = User;
            shopWindow.Show();
            this.Close();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

		private void ManageAccountsButton_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}