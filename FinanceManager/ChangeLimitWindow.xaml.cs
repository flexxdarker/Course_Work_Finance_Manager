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
    /// Interaction logic for AddLimitsWindow.xaml
    /// </summary>
    public partial class ChangeLimitWindow : Window
    {
        int NewLimit = 0;
        const int MaxLimit = 100_000;
        private IUoW uow;

        public ChangeLimitWindow(ref IUoW uow)
        {
            InitializeComponent();
            this.uow = uow;
        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // запис нового ліміта в базу щоб потім змінювати ліміт в мейн віндов

            if(!int.TryParse(LimitTextBox.Text, out NewLimit))
            { 
                MessageBox.Show($"The 'limit' field must contain only a numeric value without text or special characters! And limit cannot be greater than {MaxLimit}");
                LimitTextBox.Text = string.Empty;
                return;
            }

            try
            {        
                // додавання її в базу для подальшого виведення в список категорій
                uow.LimitRepo.Insert(new Limit { Value = NewLimit });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            LimitTextBox.Text = string.Empty;
            uow.Save();
            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

    }
}
