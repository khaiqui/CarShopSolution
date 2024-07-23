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
	/// Interaction logic for ModelsManagerWindow.xaml
	/// </summary>
	public partial class ModelsManagerWindow : Window
	{
		private ModelService _service = new();
		public ModelsManagerWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			LoadDataGrid();
		}
		private void LoadDataGrid()
		{
			ModelDataGrid.ItemsSource = null;

			ModelDataGrid.ItemsSource = _service.GetAllModels();

		}

		private void CreateButton_Click(object sender, RoutedEventArgs e)
		{
			ModelDetailWindow detail = new();
			detail.ShowDialog();
			LoadDataGrid();
		
		}

		private void UpdateButton_Click(object sender, RoutedEventArgs e)
		{
			Model selected = ModelDataGrid.SelectedItem as Model;
			if (selected == null)
			{
				MessageBox.Show("Please select a model before updating", "Select one", MessageBoxButton.OK, MessageBoxImage.Stop);
				return;
			}
			ModelDetailWindow details = new();
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
			string name = ModelNameTextBox.Text;
			var result = _service.SearchByName(name);
			ModelDataGrid.ItemsSource = null;

			ModelDataGrid.ItemsSource = result;
		}
	}
}
