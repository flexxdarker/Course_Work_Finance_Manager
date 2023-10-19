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
        public ChangeLimitWindow()
        {
            InitializeComponent();
        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(LimitTextBox.Text, out NewLimit))
            { 
                MessageBox.Show($"The 'limit' field must contain only a numeric value without text or special characters! And limit cannot be greater than {MaxLimit}");
                return;
            }
            // запис нового ліміта в базу щоб потім змінювати ліміт в мейн віндов
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
