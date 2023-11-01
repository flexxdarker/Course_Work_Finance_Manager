using EntityFramework.Entities;
using EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
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
        public Category Category {get; set; }
        public AddCategory()
        {
            InitializeComponent();
           
        }
        public AddCategory(IUoW uow)
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
                Category? tmp = uow.CategoryRepo.Get(x => x.Name == NewName).LastOrDefault();
                if (tmp != null)
                {
                    MessageBox.Show("A category with that name already exists!");
                    return;
                }
                // додавання її в базу для подальшого виведення в список категорій
                int lastCategoryId = uow.CategoryRepo.Get().Select(x => x.Id).Last();
                int lastAccountId = uow.AcountRepo.Get().Select(x => x.Id).Last();
                int lastLimitId = uow.LimitRepo.Get().Select(x => x.Id).Last();

                var prop = comboBox1.SelectedItem as PropertyInfo;
                var origColor = (Color)ColorConverter.ConvertFromString(prop.Name);
                System.Drawing.Color color= System.Drawing.Color.FromArgb(origColor.A, origColor.R, origColor.G, origColor.B);
                
                Category = new Category { Name = NewName, Summ = 500, AcountId = lastAccountId, LimitId = lastLimitId, Color = color };
                uow.CategoryRepo.Insert(Category);
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

        private void ComboBoxColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
        }
    }
}
