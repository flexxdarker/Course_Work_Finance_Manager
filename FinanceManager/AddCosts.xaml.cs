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
using System.Windows.Shapes;

namespace FinancingManager
{
    /// <summary>
    /// Interaction logic for AddCosts.xaml
    /// </summary>
    public partial class AddCosts : Window
    {
        UnitOfWork UoW = new UnitOfWork();
        private void FillComboBox()
        {
            foreach(var item in UoW.CategoryRepo.Get())
            {
                categories.Items.Add(item);
            }
        }
        public AddCosts()
        {
            InitializeComponent();
            FillComboBox();
        }

        private void AddCostBtn_Click(object sender, RoutedEventArgs e)
        {
            UoW.CostRepo.Insert(new Cost()
            {
                Name = NameTb.Text,
                CategoryId = (categories.SelectedItem as Category).Id,
                Price = Convert.ToDecimal(PriceTb.Text),
            });
            UoW.Save();
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
            this.Close();
		}
	}
}
