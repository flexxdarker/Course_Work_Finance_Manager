using EntityFramework.Entities;
using EntityFramework.Repositories;
using PropertyChanged;
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

namespace FinancingManager
{
	/// <summary>
	/// Interaction logic for ShowDetailsOfType.xaml
	/// </summary>
	/// 
	[AddINotifyPropertyChangedInterface()]
    public partial class ShowDetailsOfType : Window
    {
        UnitOfWork UoW = new UnitOfWork();
        string category;
		private void FillListBox(string category)
		{
			foreach (var item in UoW.CostRepo.Get(includeProperties: "Category").Where(x => x.Category.Name == category))
			{
				listBox.Items.Add(item);
			}
		}

		public ShowDetailsOfType()
        {
			InitializeComponent();
			category = "Health";
			FillListBox(category);
		}

		public ShowDetailsOfType(string? Category)
        {
            InitializeComponent();
			category = Category ?? "Health";
			FillListBox(category);
		}

		//private void byPrice_Selected(object sender, RoutedEventArgs e)
  //      {
  //          listBox.Items.Clear();
  //          foreach(var item in UoW.CostRepo.Get(includeProperties: "Category").Where(x => x.Category.Name == category).OrderBy(x=>x.Price))
  //          {
  //              listBox.Items.Add(item);
  //          }
  //      }

  //      private void byName_Selected(object sender, RoutedEventArgs e)
  //      {
		//	listBox.Items.Clear();
		//	foreach (var item in UoW.CostRepo.Get(includeProperties: "Category").Where(x => x.Category.Name == category).OrderBy(x => x.Name))
		//	{
		//		listBox.Items.Add(item);
		//	}
		//}
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
			UoW.CostRepo.Delete((listBox.SelectedItem as Cost).Id);
			listBox.Items.RemoveAt(listBox.SelectedIndex);
			UoW.Save();
		}

		private void byNameBtn_Click(object sender, RoutedEventArgs e)
		{
			listBox.Items.Clear();
			foreach (var item in UoW.CostRepo.Get(includeProperties: "Category").Where(x => x.Category.Name == category).OrderBy(x => x.Name))
			{
				listBox.Items.Add(item);
			}
		}

		private void byPriceBtn_Click(object sender, RoutedEventArgs e)
		{
			listBox.Items.Clear();
			foreach (var item in UoW.CostRepo.Get(includeProperties: "Category").Where(x => x.Category.Name == category).OrderBy(x => x.Price))
			{
				listBox.Items.Add(item);
			}
		}
	}
}
