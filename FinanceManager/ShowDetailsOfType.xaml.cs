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
		string theme;
		private void FillListBox(string category)
		{
			foreach (var item in UoW.CostRepo.Get(includeProperties: "Category").Where(x => x.Category.Name == category))
			{
				listBox.Items.Add(item);
			}
		}
		private void Themes(string theme)
		{
			if(theme == "Dark")
			{
				Style darkListBox = (Style)FindResource("ListBoxStyleDark");
				Style darkButton = (Style)FindResource("ButtonlStyleDark");
				Style darkLabel = (Style)FindResource("LabelStyleDark");
                Style darkDockPannel = (Style)FindResource("DockPannelStyleDark");
                listBox.Style = darkListBox;
				deleteBtn.Style = darkButton;
				deleteLabel.Style = darkLabel;
				dockPannel.Style = darkDockPannel;
			}
			else if(theme == "Light")
			{
                Style lightListBox = (Style)FindResource("ListBoxStyleLight");
                Style lightButton = (Style)FindResource("ButtonlStyleLight");
				Style lightLabel = (Style)FindResource("LabelStyleLight");
                Style lightDockPannel = (Style)FindResource("DockPannelStyleLight");
                listBox.Style= lightListBox;
				deleteBtn.Style = lightButton;
				deleteLabel.Style = lightLabel;
				dockPannel.Style = lightDockPannel;
			}
        }
		public ShowDetailsOfType()
        {
			InitializeComponent();
			theme = "Dark";
			category = "Health";
			FillListBox(category);
			Themes(theme);
		}

		public ShowDetailsOfType(string? Category, string? Theme)
        {
            InitializeComponent();
			theme = Theme ?? "Dark";
			category = Category ?? "Health";
			FillListBox(category);
			Themes(theme);
		}

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
			if(listBox.SelectedItem == null)
			{
				MessageBox.Show("You don't select item");
			}
			else
			{
				UoW.CostRepo.Delete((listBox.SelectedItem as Cost).Id);
				listBox.Items.RemoveAt(listBox.SelectedIndex);
				UoW.Save();
			}
		}

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SortByName_Selected(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            foreach (var item in UoW.CostRepo.Get(includeProperties: "Category").Where(x => x.Category.Name == category).OrderBy(x => x.Name))
            {
                listBox.Items.Add(item);
            }
        }

        private void SortByPrice_Selected(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            foreach (var item in UoW.CostRepo.Get(includeProperties: "Category").Where(x => x.Category.Name == category).OrderBy(x => x.Price))
            {
                listBox.Items.Add(item);
            }
        }
    }
}
