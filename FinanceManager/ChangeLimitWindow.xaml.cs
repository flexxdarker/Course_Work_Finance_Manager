using EntityFramework.Entities;
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
    /// Interaction logic for AddLimitsWindow.xaml
    /// </summary>
    public partial class ChangeLimitWindow : Window
    {
        int NewLimit = 0;
        const int MaxLimit = 100_000;
        private IUoW uow;
        string theme;
        private void Themes(string theme)
        {
            if(theme=="Dark")
            {
                Style darkDockPannel = (Style)FindResource("DockPannelStyleDark");
                Style darkLabel = (Style)FindResource("LabelStyleDark");
                Style darkButton = (Style)FindResource("ButtonlStyleDark");
                Style darkTextBox = (Style)FindResource("TextBoxStyleDark");
                dockPannel.Style = darkDockPannel;
                limitTextBox.Style = darkTextBox;
                cancelBtn.Style = darkButton;
                saveChangesBtn.Style = darkButton;
                newLimitLabel.Style = darkLabel;
                //BorderImage.ImageSource = new BitmapImage(new Uri("/FilesForWpf/moneyBag.jpg"));
            }
            else if(theme=="Light")
            {
                Style lightDockPannel = (Style)FindResource("DockPannelStyleLight");
                Style lightLabel = (Style)FindResource("LabelStyleLight");
                Style lightButton = (Style)FindResource("ButtonlStyleLight");
                Style lightTextBox = (Style)FindResource("TextBoxStyleLight");
                dockPannel.Style = lightDockPannel;
                limitTextBox.Style = lightTextBox;
                cancelBtn.Style = lightButton;
                saveChangesBtn.Style = lightButton;
                newLimitLabel.Style = lightLabel;
                //BorderImage.ImageSource = new BitmapImage(new Uri("/FilesForWpf/moneyBagLight.jpg"));
            }
        }
        public ChangeLimitWindow(ref IUoW uow, string Theme)
        {
            InitializeComponent();
            this.uow = uow;
            theme = Theme;
            Themes(theme);
        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // запис нового ліміта в базу щоб потім змінювати ліміт в мейн віндов

            if(!int.TryParse(limitTextBox.Text, out NewLimit))
            { 
                MessageBox.Show($"The 'limit' field must contain only a numeric value without text or special characters! And limit cannot be greater than {MaxLimit}");
                limitTextBox.Text = string.Empty;
                return;
            }

            try
            {        
                // додавання її в базу для подальшого виведення в список категорій
                uow.LimitRepo.Insert(new Limit { Value = NewLimit });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            limitTextBox.Text = string.Empty;
            uow.Save();
            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

    }
}
