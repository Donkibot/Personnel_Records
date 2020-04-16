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

namespace WPF_MD_Personnel_Records
{
    public partial class MainWindow : Window, IShowMsg
    {

        SqlManager sqlManager;
        Notifier notifier;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            notifier = new Notifier();
            this.DataContext = notifier;
        }

        private void CountingButton_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void btnImgDwnld_Click(object sender, RoutedEventArgs e)
        {
            string path = GetPathOfPhoto();
            BitmapImage image = new BitmapImage();
            try
            {
                image.BeginInit();
                image.UriSource = new Uri(path, UriKind.Absolute);
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Height.ToString();
                picBox.Source = image;
                picBox.Tag = image;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка завантаження");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlManager = new SqlManager(this);
            btn_ImgDwnld.Visibility = Visibility.Hidden;
            BitmapImage image = new BitmapImage();
            try
            {
                image.BeginInit();
                image.UriSource = new Uri("Resources/Images/default-user.png", UriKind.Relative);
                image.EndInit();
                image.Height.ToString();
                picBox.Source = image;
            }
            catch (Exception ex)
            {
                picBox.Source = null;
            }
            positionBox.Visibility = Visibility.Hidden;
            UpdateList();
            FillPositionBox();
        }

        public void UpdateList()
        {
            listBox.Items.Clear();
            foreach (Member m in sqlManager.GetAllMembers())
            {
                string info = $"{m.surname} {m.name.Substring(0, 1)}.{m.patronymic.Substring(0, 1)}. \t-\t {m.position.position}";

                MaterialDesignThemes.Wpf.Chip chip = new MaterialDesignThemes.Wpf.Chip();
                chip.Content = info;
                chip.Tag = m;
                chip.Click += chip_Selected;
                Image image = new Image();
                image.Source = m.photo.GetPhoto();
                chip.Icon = image;
                listBox.Items.Add(chip);
            }
        }

        string GetPathOfPhoto()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Images (*.txt)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            fileDialog.ShowDialog();
            return fileDialog.FileName;
        }


        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            DB_Image photo = sqlManager.GetImageFromDB(1);
            SetPhotoToViewer(photo.GetPhoto());
        }


        void SetPhotoToViewer(BitmapImage img)
        {
            picBox.Source = null;
            try
            {
                picBox.Source = img;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка завантаження фото");
            }
        }

        public void ShowMsg(string msg)
        {
            TB_Notes.AppendText(msg);
        }


        public bool canEdit { get; set; } = false;
        void ChangeEditStatus()
        {
            canEdit = !canEdit;

            if (!canEdit)
            {
                btn_ImgDwnld.Visibility = Visibility.Hidden;
                positionBox.Visibility = Visibility.Hidden;
                TB_Position.Visibility = Visibility.Visible;
                updateMember();
            }
            else
            {
                btn_ImgDwnld.Visibility = Visibility.Visible;
                positionBox.Visibility = Visibility.Visible;
                TB_Position.Visibility = Visibility.Hidden;
            }

        }

        void updateMember()
        {
            DB_Image img = null;
            Position position = new Position((int)(positionBox.SelectedItem as ComboBoxItem).Tag, (positionBox.SelectedItem as ComboBoxItem).Content.ToString());
            TB_Position.Text = position.position;
            Member member = new Member((int)label_id.Tag, TB_Name.Text, TB_Surname.Text, TB_Patromymic.Text, TB_Phone.Text, TB_Sex.Text, position, img, TB_Notes.Text);
            sqlManager.updateMember(member);
            UpdateList();
        }

        void FillPositionBox()
        {
            List<Position> positions = sqlManager.GetAllPosition();
            foreach (Position pos in positions)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = pos.position;
                item.Tag = pos.id;
            positionBox.Items.Add(item);
            }
        }

        private void updBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeEditStatus();
            notifier.canEdit = !canEdit;
            btn_Upd.Content = canEdit ? "Підтвердити" : "Оновити";
        }

        private void chip_Selected(object sender, RoutedEventArgs e)
        {
            if (!btn_Del.IsEnabled)
            {
                btn_Del.IsEnabled = true;
                btn_Upd.IsEnabled = true;
            }
            Member member = ((sender as MaterialDesignThemes.Wpf.Chip).Tag as Member);
            TB_Name.Text = member.name;
            TB_Surname.Text = member.surname;
            TB_Patromymic.Text = member.patronymic;
            TB_Phone.Text = member.phoneNum;
            TB_Position.Text = member.position.position;
            TB_Sex.Text = member.sex;
            TB_Notes.Text = member.note;
            BitmapImage photo = member.photo.GetPhoto();
            label_id.Tag = member.id;
            foreach (ComboBoxItem item in positionBox.Items)
            {
                if ((int)item.Tag == member.position.id) positionBox.SelectedItem = item;
            }
            
            SetPhotoToViewer(photo);
        }
    }
}

