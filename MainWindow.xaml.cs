using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using WPF_MD_Personnel_Records.Pages;

namespace WPF_MD_Personnel_Records
{
    public partial class MainWindow : Window, IShowMsg
    {

        Notifier notifier;
        Page1 addingPage;
        SqlManager sqlManager;
        string iconPath = @"Icon/account-icon.png";
        public MainWindow()
        {
            InitializeComponent();
            LoadIcon();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            notifier = new Notifier();
            DataContext = notifier;
            sqlManager = new SqlManager();
        }

        void LoadIcon()
        {
            if (!File.Exists(iconPath)) return;
            BitmapImage iconImage = new BitmapImage();
            iconImage.BeginInit();
            iconImage.UriSource = new Uri(iconPath, UriKind.Relative);
            iconImage.EndInit();
            this.Icon = iconImage;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitPage();
        }

        void InitPage()
        {
            addingPage = new Page1(notifier);
            framePage.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            framePage.Content = addingPage;
        }

        public void ShowMsg(string msg)
        {
            TB_Notes.AppendText(msg);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindMembers();
        }

        private void TB_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindMembers();
        }

        void FindMembers()
        {
            string name = TB_Search.Text;
            string position = null;
            if ((CB_Position_Find.SelectedItem as Position) != null)
            {
                position = (CB_Position_Find.SelectedItem as Position).position;
            }
            if (string.IsNullOrWhiteSpace(name)) name = "%";
            if (string.IsNullOrWhiteSpace(position) || position == "Всі") position = "%";
            List<Member> memberFound = sqlManager.GetAllMembersFound(name, position);
            notifier.FilterMemberList(memberFound);
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            // addingPage.positionBox.SelectedIndex = 1;
        }
    }
}

