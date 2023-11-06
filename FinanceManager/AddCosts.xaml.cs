using EntityFramework.Entities;
using EntityFramework.Repositories;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace FinancingManager
{
    /// <summary>
    /// Interaction logic for AddCosts.xaml
    /// </summary>
    public partial class AddCosts : Window
    {
        UnitOfWork UoW = new UnitOfWork();
        string theme;
        private void FillComboBox()
        {
            foreach(var item in UoW.CategoryRepo.Get())
            {
                categories.Items.Add(item);
            }
        }
        private void Themes(string theme)
        {
            if (theme == "Dark")
            {
                Style darkDockPannel = (Style)FindResource("DockPannelStyleDark");
                Style darkLabel = (Style)FindResource("LabelStyleDark");
                Style darkTextBox = (Style)FindResource("TextBoxStyleDark");
                Style darkButton = (Style)FindResource("ButtonlStyleDark");
                categoryLabel.Style = darkLabel;
                nameLabel.Style = darkTextBox;
                priceLabel.Style = darkButton;
                NameTb.Style = darkButton;
                PriceTb.Style = darkButton;
                addCostBtn.Style = darkButton;
                cancelBtn.Style = darkButton;
                dockPannel.Style = darkButton;
            }
            else if(theme == "Light")
            {
                Style lightDockPannel = (Style)FindResource("DockPannelStyleLight");
                Style lightLabel = (Style)FindResource("LabelStyleLight");
                Style lightTextBox = (Style)FindResource("TextBoxStyleLight");
                Style lightButton = (Style)FindResource("ButtonlStyleLight");
                categoryLabel.Style = lightLabel;
                nameLabel.Style = lightLabel;
                priceLabel.Style = lightLabel;
                NameTb.Style = lightTextBox;
                PriceTb.Style = lightTextBox;
                addCostBtn.Style = lightButton;
                cancelBtn.Style = lightButton;
                dockPannel.Style = lightDockPannel;
            }
        }
        public AddCosts(string Theme)
        {
            InitializeComponent();
            FillComboBox();
            this.theme = Theme;
            Themes(theme);
        }

        private void AddCostBtn_Click(object sender, RoutedEventArgs e)
        {
            var MoneySpentSumm = UoW.CategoryRepo.Get().Select(x => x.Summ).Sum();
         if(MoneySpentSumm + Convert.ToDecimal(PriceTb.Text) <  UoW.LimitRepo.Get().Select(x => x.Value).Last())
         { 

            UoW.CostRepo.Insert(new Cost()
            {
                Name = NameTb.Text,
                CategoryId = (categories.SelectedItem as Category).Id,
                Price = Convert.ToDecimal(PriceTb.Text),
            });
            UoW.Save();
            this.Close();
         }
         else
            {
                MessageBox.Show("YOU HAVE EXCEEDED THE LIMIT");
            }
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
            this.Close();
		}
	}
}
