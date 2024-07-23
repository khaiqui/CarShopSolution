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
	/// Interaction logic for DiscountDetailWindow.xaml
	/// </summary>
	public partial class DiscountDetailWindow : Window
	{
		private DiscountService _service = new();
		public Discount Selected {  get; set; }
		public DiscountDetailWindow()
		{
			InitializeComponent();
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			DiscountlIdTextBox.IsEnabled = false;
			ModelLabel.Content = "Create Discount";
			if (Selected != null)
			{
				ModelLabel.Content = "Update Discount";
				DiscountlIdTextBox.Text = Selected.DiscountId.ToString();
				RateTextBox.Text = Selected.DiscountRate.ToString();

			}
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			Discount x = new();
				x.DiscountRate = int.Parse(RateTextBox.Text);
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
