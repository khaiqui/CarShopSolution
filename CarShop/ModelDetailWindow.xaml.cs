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
	/// Interaction logic for ModelDetailWindow.xaml
	/// </summary>
	public partial class ModelDetailWindow : Window
	{
		private ModelService _service = new();
		public Model Selected { get; set; } = null;
		public ModelDetailWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ModelIdTextBox.IsEnabled = false;
			ModelLabel.Content = "Create Model";
			if (Selected != null)
			{
				ModelLabel.Content = "Update Model";
				ModelIdTextBox.Text = Selected.ModelId.ToString();
				ModelNameTextBox.Text = Selected.ModelName;	

			}
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			Model x = new();
			x.ModelName = ModelNameTextBox.Text;
			if (Selected == null)
			{
				_service.Create(x);
			}
			else
				_service.Update(x);
			this.Close();
		}

		private void QuitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
