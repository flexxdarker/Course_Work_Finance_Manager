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
        IUoW uow = new UnitOfWork();
        ChangeLimitWindow? changeLimitWindow;
        AddCategory? addCategory;
        const decimal defaultLimit = 10000;
        decimal limit = 1;
        string theme;
        void AddDiagram(Category item)
        {

            Diagram.Series.Add(new PieSeries
            {

                Title = item.Name,
                Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(item.Color.R, item.Color.G, item.Color.B)),
                StrokeThickness = 0,
                Values = new ChartValues<double>
                {
                       (Convert.ToDouble((item.Summ / 100)))
                }
            });
        }
        void CalculateSpentMoney()
        {
            var MoneySpentSumm = uow.CategoryRepo.Get().Select(x => x.Summ).Sum();
            moneySpentLabel.Content = MoneySpentSumm;
        }
        public MainWindow()
        {
            InitializeComponent();

            Random r = new Random();

            foreach (var item in uow.CategoryRepo.Get())
            {
                AddDiagram(item);
            }

           
            SetListBoxesOnStart();

            SetLimit();
            FillListBoxes();
            ItemSource();
            CalculateSpentMoney();

        }
        void SetListBoxesOnStart()
        {
            var Categories = uow.CategoryRepo.Get().ToList();
            var costs = uow.CostRepo.Get().ToList();
            for (int i = 0; i < categories.Count(); ++i)
            {
                if (categories[i].Name == Categories[i].Name)
                {
                    for (int j = 0; j < costs.Count; j++)
                    {
                        if (Categories[i].Id == costs[j].CategoryId)
                        {
                            categories[i].Summ += costs[j].Price;
                            categories[i].Persent = (categories[i].Summ * 100) / limit;
                        }
                    }
                }
            }
        }
        private void ItemSource()
        {
            categoriesListBox.ItemsSource = categories;
            moneyListBox.ItemsSource = categories;
            percentsListBox.ItemsSource = categories;
        }
        private void SetLimit()
        {
            limit = defaultLimit;
            var limits = uow.LimitRepo.Get();
            if (limits.Count() == 0)
                limitLabel.Content = defaultLimit;
            else
            {
                limit = uow.LimitRepo.Get().Select(x => x.Value).Last();
                limitLabel.Content = limit;
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
            changeLimitWindow = new ChangeLimitWindow(ref uow, theme);
            changeLimitWindow.ShowDialog();

            // витягання з бази останнього елементу з таблиці лімітів
            try
            {
                var lastLimit = uow.LimitRepo.Get().Last();
                limit = lastLimit.Value;
                limitLabel.Content = limit;

                FillListBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            uow.Save();
        }
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                addCategory = new AddCategory(uow, theme);
                if (addCategory.ShowDialog() == true)
                {
                    try
                    {
                        //витягання з бази доданої категорії 
                        var lastCategory = uow.CategoryRepo.Get().Last();

                        //виведення її в список категорій
                        limit = uow.LimitRepo.Get().Select(x => x.Value).Last();

                        categories.Add(new CategoryView(lastCategory.Name, lastCategory.Summ, (lastCategory.Summ * 100 / limit)));
                        ItemSource();
                        FillListBoxes();

                        AddDiagram(lastCategory);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    uow.Save();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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
            if (categoriesListBox.SelectedItem != null)
                RemoveIdList = categoriesListBox.SelectedIndex;
            else if (moneyListBox.SelectedItem != null)
                RemoveIdList = moneyListBox.SelectedIndex;
            else if (percentsListBox.SelectedItem != null)
                RemoveIdList = percentsListBox.SelectedIndex;
            else
            { MessageBox.Show("Select a category to delete!"); return; }

            CategoryView selectedCategory = (CategoryView)categoriesListBox.SelectedItem;

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
             var pie = Diagram.Series.FirstOrDefault(x => x.Title == SelectedCategory.Name);
            Diagram.Series.Remove(pie);
        }

        private void AddCost_Click(object sender, RoutedEventArgs e)
        {
            AddCosts addcost = new AddCosts(theme);
            if (addcost.ShowDialog() == true)
            {
                var currentCategory = uow.CategoryRepo.Get().Where(x => x.Id == addcost.currentCategotyId).Last();
                int id = 0;
                foreach (var item in categoriesListBox.Items)
                {
                    if (item == currentCategory.Name)
                    {
                        id = categoriesListBox.Items.IndexOf(item);
                    }
                }
                categories[id].Summ = currentCategory.Summ;
                categories[id].Persent = currentCategory.Summ * 100 / limit;
                ItemSource();
                FillListBoxes();
                CalculateSpentMoney();
            }
        }

        private void CategoriesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedObject = categoriesListBox.SelectedItem as CategoryView;

            ShowDetailsOfType showDetails = new ShowDetailsOfType(selectedObject.Name, theme);
            showDetails.ShowDialog();

            ItemSource();
            FillListBoxes();
            CalculateSpentMoney();

        }

        private void DarkThemeBtn_Click(object sender, RoutedEventArgs e)
        {
            theme = "Dark";
            Style darkLabel = (Style)FindResource("LabelStyleDark");
            Style darkButton = (Style)FindResource("ButtonlStyleDark");
            Style darkListBox = (Style)FindResource("ListBoxStyleDark");
            Style darkToolBar = (Style)FindResource("ToolBarStyleDark");
            Style darkDockPannel = (Style)FindResource("DockPannelStyleDark");
            moneyLabel.Style = darkLabel;
            moneySpentLabel.Style = darkLabel;
            limitDollarLabel.Style = darkLabel;
            dollarSpentLabel.Style = darkLabel;
            categoryLabel.Style = darkLabel;
            percentsLabel.Style = darkLabel;
            limitLabel.Style = darkLabel;
            currentLimitLabel.Style = darkLabel;
            spentLabel.Style = darkLabel;
            addCategoryBtn.Style = darkButton;
            changeLimitBtn.Style = darkButton;
            addCostBtn.Style = darkButton;
            deleteCategoryBtn.Style = darkButton;
            exitBtn.Style = darkButton;
            lightThemeBtn.Style = darkButton;
            darkThemeBtn.Style = darkButton;
            limitHistoryBtn.Style = darkButton;
            sortByNameBtn.Style = darkButton;
            sortByMoneyBtn.Style = darkButton;
            sortByPercentsBtn.Style = darkButton;
            moneyListBox.Style = darkListBox;
            percentsListBox.Style = darkListBox;
            categoriesListBox.Style = darkListBox;
            mainToolBar.Style = darkToolBar;
            secondToolBar.Style = darkToolBar;
            dockPannel.Style = darkDockPannel;
        }

        private void LightThemeBtn_Click(object sender, RoutedEventArgs e)
        {
            theme = "Light";
            Style lightLabel = (Style)FindResource("LabelStyleLight");
            Style lightButton = (Style)FindResource("ButtonlStyleLight");
            Style lightListBox = (Style)FindResource("ListBoxStyleLight");
            Style lightToolBar = (Style)FindResource("ToolBarStyleLight");
            Style lightDockPannel = (Style)FindResource("DockPannelStyleLight");
            moneyLabel.Style = lightLabel;
            moneySpentLabel.Style = lightLabel;
            limitDollarLabel.Style = lightLabel;
            dollarSpentLabel.Style = lightLabel;
            categoryLabel.Style = lightLabel;
            percentsLabel.Style = lightLabel;
            limitLabel.Style = lightLabel;
            currentLimitLabel.Style = lightLabel;
            spentLabel.Style = lightLabel;
            addCategoryBtn.Style = lightButton;
            changeLimitBtn.Style = lightButton;
            addCostBtn.Style = lightButton;
            deleteCategoryBtn.Style = lightButton;
            exitBtn.Style = lightButton;
            lightThemeBtn.Style = lightButton;
            darkThemeBtn.Style = lightButton;
            limitHistoryBtn.Style = lightButton;
            sortByNameBtn.Style = lightButton;
            sortByMoneyBtn.Style = lightButton;
            sortByPercentsBtn.Style = lightButton;
            moneyListBox.Style = lightListBox;
            percentsListBox.Style = lightListBox;
            categoriesListBox.Style = lightListBox;
            mainToolBar.Style = lightToolBar;
            secondToolBar.Style = lightToolBar;
            dockPannel.Style = lightDockPannel;
        }
    }
}