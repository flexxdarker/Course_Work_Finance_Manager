using EntityFramework.Entities;
using EntityFramework.Repositories;
using FinancingManager;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<CategoryView> categories = new ObservableCollection<CategoryView>();

        private IUoW uow = new UnitOfWork();
        ChangeLimitWindow? changeLimitWindow;
        AddCategory? addCategory;
        const decimal defaultLimit = 10000;
        decimal limit;
        
        public MainWindow()
        {
            InitializeComponent();
            Diagram.Series.Add(new PieSeries { Title= "la",Fill=Brushes.LightGray, StrokeThickness = 0,Values = new ChartValues<double> {20.0} });
            Diagram.Series.Add(new PieSeries { Title = "aasd", Fill = Brushes.DarkGray, StrokeThickness = 0, Values = new ChartValues<double> { 30.0 } });
            Diagram.Series.Add(new PieSeries { Title = "la", Fill = Brushes.Gray, StrokeThickness = 0, Values = new ChartValues<double> { 10.0 } });
            Diagram.Series.Add(new PieSeries { Title = "la", Fill = Brushes.White, StrokeThickness = 0, Values = new ChartValues<double> { 40.0 } });


            limit = defaultLimit;
            var limits = uow.LimitRepo.Get();
            if (limits.Count() == 0)
                LimitLabel.Content = defaultLimit;
            else
            {
                limit = uow.LimitRepo.Get().Select(x => x.Value).Last();
                LimitLabel.Content = limit;
            }

            FillListBoxes();
            ItemSource();
        }
        private void ItemSource()
        {

            CategoriesListBox.ItemsSource = categories;
            MoneyListBox.ItemsSource = categories;
            PercentsListBox.ItemsSource = categories;
        }

        private void FillListBoxes()
        {
            PercentsListBox.Items.Clear();
            CategoriesListBox.Items.Clear();
            MoneyListBox.Items.Clear();
            var CategoryNames = uow.CategoryRepo.Get().Select(x => x.Name).ToList();
            var Money = uow.CategoryRepo.Get().Select(x => x.Summ).ToList();

            var Categories = uow.CategoryRepo.Get().ToList();
            for (int i = 0; i < CategoryNames.Count(); i++)
            {
                categories.Add(new CategoryView(CategoryNames[i], Money[i], (Categories[i].Summ * 100) / limit ));
            }
            
        }
        private void ChangeLimit_Click(object sender, RoutedEventArgs e)
        {
            changeLimitWindow = new ChangeLimitWindow(ref uow);
            changeLimitWindow.ShowDialog();

            // витягання з бази останнього елементу з таблиці лімітів
            try
            {
                var lastLimit = uow.LimitRepo.Get().Last();
                limit = lastLimit.Value;
                LimitLabel.Content = limit;

                FillListBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            uow.Save();
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
                

                string summ = (lastCategory.Summ % 1 == 0) ? ($"{lastCategory.Summ}.00") : (lastCategory.Summ.ToString());
                MoneyListBox.Items.Add(summ);
                
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

        }

		private void Diagram_Loaded(object sender, RoutedEventArgs e)
		{

		}

        private void SortByName(object sender, RoutedEventArgs e)
        {       
            categories = new (categories.OrderBy(x => x.Name));
            ItemSource();
        }

        private void SortByMoney(object sender, RoutedEventArgs e)
        {
            categories = new(categories.OrderBy(x => x.Summ));
            ItemSource();
        }

        private void SortByPercents(object sender, RoutedEventArgs e)
        {
            categories = new(categories.OrderBy(x => x.Persent));
            ItemSource();
        }
    }
}
