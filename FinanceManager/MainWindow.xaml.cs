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
        decimal limit = 1;
           void AddDiagram(Category item)
           {   
            
                Diagram.Series.Add(new PieSeries { 
                   
                   Title= item.Name,
                   Fill= new SolidColorBrush(System.Windows.Media.Color.FromRgb(item.Color.R,item.Color.G,item.Color.B)), 
                   StrokeThickness = 0,
                   Values = new ChartValues<double> 
                   { 
                       (Convert.ToDouble((item.Summ / 100)))
                   }
               });
           }
        public MainWindow()
        {
            InitializeComponent();

            Random r = new Random();
      
            foreach (var item in uow.CategoryRepo.Get())
            {
                AddDiagram(item);
            }
       
            Diagram.Series.Add(new PieSeries { Title = "la", Fill = Brushes.LightGray, StrokeThickness = 0, Values = new ChartValues<double> { 20.0 } });
            Diagram.Series.Add(new PieSeries { Title = "aasd", Fill = Brushes.DarkGray, StrokeThickness = 0, Values = new ChartValues<double> { 30.0 } });
            Diagram.Series.Add(new PieSeries { Title = "la", Fill = Brushes.Gray, StrokeThickness = 0, Values = new ChartValues<double> { 10.0 } });
            Diagram.Series.Add(new PieSeries { Title = "la", Fill = Brushes.White, StrokeThickness = 0, Values = new ChartValues<double> { 40.0 } });

            SetLimit();
            FillListBoxes();
            ItemSource();
        }
        private void ItemSource()
        {

            CategoriesListBox.ItemsSource = categories;
            MoneyListBox.ItemsSource = categories;
            PercentsListBox.ItemsSource = categories;
        }
        private void SetLimit()
        {
            limit = defaultLimit;
            var limits = uow.LimitRepo.Get();
            if (limits.Count() == 0)
                LimitLabel.Content = defaultLimit;
            else
            {
                limit = uow.LimitRepo.Get().Select(x => x.Value).Last();
                LimitLabel.Content = limit;
            }
        }
        private void FillListBoxes()
        {
            categories.Clear();
            ItemSource();
            var CategoryNames = uow.CategoryRepo.Get().Select(x => x.Name).ToList();
            var Money = uow.CategoryRepo.Get().Select(x => x.Summ).ToList();

            var Categories = uow.CategoryRepo.Get().ToList();
            for (int i = 0; i < CategoryNames.Count(); i++)
            {
                categories.Add(new CategoryView(CategoryNames[i], Money[i], (Categories[i].Summ * 100) / limit));
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
            
            addCategory = new AddCategory(uow);
            addCategory.ShowDialog();

            try
            {
                //витягання з бази доданої категорії 
                var lastCategory = uow.CategoryRepo.Get().Last();

                //виведення її в список категорій
                limit = uow.LimitRepo.Get().Select(x => x.Value).Last();

                categories.Add(new CategoryView(lastCategory.Name, lastCategory.Summ, (lastCategory.Summ * 100 / limit)));
                ItemSource();

                AddDiagram(lastCategory);
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

        private void SortByName(object sender, RoutedEventArgs e)
        {
            categories = new(categories.OrderBy(x => x.Name));
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


        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            int RemoveIdList = 0;
            if (CategoriesListBox.SelectedItem != null)
                RemoveIdList = CategoriesListBox.SelectedIndex;
            else if (MoneyListBox.SelectedItem != null)
                RemoveIdList = MoneyListBox.SelectedIndex;
            else if (PercentsListBox.SelectedItem != null)
                RemoveIdList = PercentsListBox.SelectedIndex;
            else
            { MessageBox.Show("Select a category to delete!"); return; }

            CategoryView selectedCategory = (CategoryView)CategoriesListBox.SelectedItem;

            categories.RemoveAt(RemoveIdList);
            ItemSource();

            Category? SelectedCategory = uow.CategoryRepo.Get().FirstOrDefault(x => x.Name == selectedCategory.Name);
            var Costs = uow.CostRepo.Get().Where(x => x.CategoryId == SelectedCategory.Id);
            foreach (var cost in Costs)
            {
                uow.CostRepo.Delete(cost.Id);
            }
            uow.CategoryRepo.Delete(SelectedCategory.Id);
            uow.Save();
        }

        private void CategoriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedObject = CategoriesListBox.SelectedItem.ToString();

            ShowDetailsOfType showDetails =  new ShowDetailsOfType(selectedObject);
            showDetails.ShowDialog();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ShowExpenses_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        private void AddCost_Click(object sender, RoutedEventArgs e)
		    {
            AddCosts addcost = new AddCosts();
            addcost.ShowDialog();
	    	}
	  }
}

