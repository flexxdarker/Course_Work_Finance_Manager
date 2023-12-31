﻿using EntityFramework.Entities;
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
        private IUoW? uow;
        public Category? Category {get; set; }
        string theme;
        private void Themes(string theme)
        {
            if (theme == "Dark")
            {
                Style darkButton = (Style)FindResource("ButtonlStyleDark");
                Style darkDockPannel = (Style)FindResource("DockPannelStyleDark");
                Style darkLabel = (Style)FindResource("LabelStyleDark");
                Style darkTextBox = (Style)FindResource("TextBoxStyleDark");
                categoryNameLabel.Style = darkLabel;
                categoryTextBox.Style = darkTextBox;
                saveBtn.Style = darkButton;
                cancelBtn.Style = darkButton;
                dockPannel.Style = darkDockPannel;
            }
            else if(theme == "Light")
            {
                Style lightButton = (Style)FindResource("ButtonlStyleLight");
                Style lightDockPannel = (Style)FindResource("DockPannelStyleLight");
                Style lightLabel = (Style)FindResource("LabelStyleLight");
                Style lightTextBox = (Style)FindResource("TextBoxStyleLight");
                categoryNameLabel.Style = lightLabel;
                categoryTextBox.Style = lightTextBox;
                saveBtn.Style = lightButton;
                cancelBtn.Style = lightButton;
                dockPannel.Style = lightDockPannel;
            }
        }
        public AddCategory()
        {
            InitializeComponent();
            theme = "Dark";
        }
        public AddCategory(IUoW uow, string Theme)
        {
            InitializeComponent();
            this.uow = uow;
            theme = Theme;
            Themes(theme);
        }
        private void SaveCategory_Click(object sender, RoutedEventArgs e)
        {
            string NewName = categoryTextBox.Text;
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
                
                Category = new Category { Name = NewName, Summ = 0, AcountId = lastAccountId, LimitId = lastLimitId, Color = color };
                uow.CategoryRepo.Insert(Category);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            categoryTextBox.Text = string.Empty;

            uow.Save();
            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ComboBoxColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
        }
    }
}
