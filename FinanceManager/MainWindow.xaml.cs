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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        public MainWindow()
        {
            InitializeComponent();
            Diagram.Series.Add(new PieSeries { Title= "la",Fill=Brushes.LightGray, StrokeThickness = 0,Values = new ChartValues<double> {20.0} });
            Diagram.Series.Add(new PieSeries { Title = "aasd", Fill = Brushes.DarkGray, StrokeThickness = 0, Values = new ChartValues<double> { 30.0 } });
            Diagram.Series.Add(new PieSeries { Title = "la", Fill = Brushes.Gray, StrokeThickness = 0, Values = new ChartValues<double> { 10.0 } });
            Diagram.Series.Add(new PieSeries { Title = "la", Fill = Brushes.White, StrokeThickness = 0, Values = new ChartValues<double> { 40.0 } });


            decimal limit = defaultLimit;
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
                CategoriesListBox.Items.Add(category);
            }
            foreach (var money in Money)
            {
                MoneyListBox.Items.Add(money);
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
                CategoriesListBox.Items.Add(lastCategory.Name);
                MoneyListBox.Items.Add(lastCategory.Summ);
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

        }

		private void Diagram_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}
}
