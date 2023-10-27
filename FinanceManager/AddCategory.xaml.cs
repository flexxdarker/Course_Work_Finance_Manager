using EntityFramework.Entities;
using EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        private IUoW uow;

        public AddCategory(ref IUoW uow)
        {
            InitializeComponent();
            this.uow = uow;
        }
        private void SaveCategory_Click(object sender, RoutedEventArgs e)
        {
            string NewName = CategoryTextBox.Text;
            // перевірка чи є категорія в базі
            try
            {
                //var tmp = uow.CategoryRepo.Get(x => x.Name == NewName);
                //if (tmp.Count() != 0)
                //{
                //    MessageBox.Show("A category with that name already exists!");
                //    return;
                //}

                // додавання її в базу для подальшого виведення в список категорій
                int lastCategoryId = uow.CategoryRepo.Get(x => x.Id != -1).Max(x => x.Id);
                int lastAccountId = uow.AcountRepo.Get(x => x.Id != -1).Max(x => x.Id);
                int lastLimitId = uow.LimitRepo.Get(x => x.Id != -1).Max(x => x.Id);
                uow.CategoryRepo.Insert(new Category { Name = NewName, Summ = 0, AcountId = lastAccountId, LimitId = lastLimitId });
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            CategoryTextBox.Text = string.Empty;
            uow.Save();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
