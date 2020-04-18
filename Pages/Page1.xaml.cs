using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WPF_MD_Personnel_Records.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Notifier notifier;

        public Page1(Notifier notif)
        {
            InitializeComponent();
            notifier = notif;
            DataContext = notifier;
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
        }

        private void TB_Name_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            
        }

        private void RadioBtnSex_Checked(object sender, RoutedEventArgs e)
        {
                RadioBtnMan.IsChecked = false;
            RadioBtnWoman.IsChecked = false;
            (sender as RadioButton).IsChecked = true;
        }
    }
}
