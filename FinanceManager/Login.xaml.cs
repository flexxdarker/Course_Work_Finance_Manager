using EntityFramework.Entities;
using EntityFramework.Repositories;
using FinanceManager;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        UnitOfWork UoW = new UnitOfWork();
        public Login()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            Acount acount = UoW.AcountRepo.GetByID(1);
            if (acount.Login == loginTb.Text && acount.Password == passwordTb.Text) 
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
        }
    }
}
