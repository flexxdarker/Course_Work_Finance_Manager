using EntityFramework.Repositories;
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
    /// Interaction logic for ShowDetailsOfType.xaml
    /// </summary>
    public partial class ShowDetailsOfType : Window
    {
        UnitOfWork UoW = new UnitOfWork();
        public ShowDetailsOfType()
        {
            InitializeComponent();
        }

        private void byPrice_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void byDate_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void byName_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
