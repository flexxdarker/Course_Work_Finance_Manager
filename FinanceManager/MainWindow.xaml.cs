using EntityFramework.Entities;
using EntityFramework.Repositories;
using FinancingManager;
using LiveCharts;
using LiveCharts.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinanceManager
{
 


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private IUoW uow = new UnitOfWork();
        ChangeLimitWindow? changeLimitWindow;
        AddCategory? addCategory;
        const decimal defaultLimit = 10000;
        decimal limit = 1;
        public MainWindow()
        {
            InitializeComponent();

            Random r = new Random();
           // Color randomColor = (Color)random.Next(Enum.GetNames(typeof(Color)).Length);
 
            //var c = (System.Windows.Media.Color)Enum.ToObject(typeof(System.Windows.Media.Color), random.Next(Enum.GetNames(typeof(System.Windows.Media.Color)).Length));

            foreach (var item in uow.CategoryRepo.Get())
            {
               Diagram.Series.Add(new PieSeries { 
                   
                   Title= item.Name,
                   Fill= new SolidColorBrush(System.Windows.Media.Color.FromRgb(item.Color.R,item.Color.G,item.Color.B)) /*new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)))*/, 
                   StrokeThickness = 0,
                   Values = new ChartValues<double> 
                   { 
                       (Convert.ToDouble((item.Summ / 100)))
                   }
               });
            }
       

            limit = defaultLimit;
            var limits = uow.LimitRepo.Get();
            if (limits.Count() == 0)
                LimitLabel.Content = defaultLimit;
            else
            {
                limit = uow.LimitRepo.Get().Select(x => x.Value).Last();
                LimitLabel.Content = limit;
            }

            var CategoryNames = uow.CategoryRepo.Get().Select(x => x.Name);
            var Money = uow.CategoryRepo.Get().Select(x => x.Summ);
            //CategoriesListBox.ItemsSource = CategoryNames;
            //MoneyListBox.ItemsSource = Money;
            foreach (var category in CategoryNames)
            {
                CategoriesListBox.Items.Add(category.ToString());
            }
            foreach (var money in Money)
            {
                MoneyListBox.Items.Add(money.ToString());
            }
            var Categories = uow.CategoryRepo.Get();
            foreach (var item in Categories)
            {
                PercentsListBox.Items.Add($"{(item.Summ * 100) / limit} %");
            }
        }
        
        private void ChangeLimit_Click(object sender, RoutedEventArgs e)
        {
            changeLimitWindow = new ChangeLimitWindow();
            changeLimitWindow.ShowDialog();
            // витягання з бази останнього елементу з таблиці лімітів
        }

        private void ShowExpenses_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // запуск вікна з виведенням покупок по категорії
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            
            addCategory = new AddCategory(ref uow);
            addCategory.ShowDialog();

            try
            {
                //витягання з бази доданої категорії 
                var lastCategory = uow.CategoryRepo.Get().Last();

                //виведення її в список категорій
                limit = uow.LimitRepo.Get().Select(x => x.Value).Last();
                CategoriesListBox.Items.Add(lastCategory.Name);
                //MoneyListBox.Items.Add(lastCategory.Summ);

                string summ = (lastCategory.Summ % 1 == 0) ? ($"{lastCategory.Summ}.00") : (lastCategory.Summ.ToString());
                MoneyListBox.Items.Add(summ);
                //number % 1 == 0

                //
                PercentsListBox.Items.Add($"{(lastCategory.Summ * 100) / limit} %");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            uow.Save();
        }

        private void LimitHistory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

		private void Diagram_Loaded(object sender, RoutedEventArgs e)
		{

		}

        private void Sort(ListBox listBox ) 
        {
            List<string> list = new List<string>();
            foreach (var item in listBox.Items)
            {
                list.Add((string)item);
            }
            listBox.Items.Clear();
            list.Sort();
            listBox.ItemsSource = list;
        }
        private void SortByName(object sender, RoutedEventArgs e)
        {
            Sort(CategoriesListBox);
        }

        private void SortByMoney(object sender, RoutedEventArgs e)
        {
            Sort(MoneyListBox);
        }

        private void SortByPercents(object sender, RoutedEventArgs e)
        {
            Sort(PercentsListBox);
        }

        private void CategoriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedObject = CategoriesListBox.SelectedItem.ToString();

            ShowDetailsOfType showDetails =  new ShowDetailsOfType(selectedObject);
            showDetails.ShowDialog();
        }
    }
}
