﻿using System;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddLimit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AddLimit_Click");
        }

        private void ShowCostst_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("ShowCostst_DoubleClick");
        }
    }
}
