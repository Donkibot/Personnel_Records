using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPF_MD_Personnel_Records
{
  public class PhotoClass : INotifyPropertyChanged
    {
        public string photoPath;
        public string defaultPath = @"Photos\default.png";
        Image image;
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                Notify("Image");
            }
        }



        public string PhotoPath
        { 
            get => photoPath; 
            set {
                photoPath = value;
                Notify("path");
            }
        }

        public string FullPhotoPath
        {
            get
            {
                string fullPath;
                if (File.Exists(PhotoPath) && PhotoPath !=null)
                    fullPath = Path.GetFullPath(PhotoPath);
                else
                    fullPath = Path.GetFullPath(defaultPath);
                return fullPath;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void Notify(string parameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(parameter));
            }
        }


        public PhotoClass(string path)
        {
            this.PhotoPath = path;
            Image tempImage = new Image();
            tempImage.Source = GetPhoto();
            Image = tempImage;
        }

        public BitmapImage GetPhoto()
        {
            string fullPath;
            if (File.Exists(PhotoPath))
                fullPath = Path.GetFullPath(PhotoPath);
            else
                fullPath = Path.GetFullPath(defaultPath);

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(fullPath, UriKind.Absolute);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();

            image.Height.ToString();
            return image;
        }

        public PhotoClass GetInstance(string path)
        {
            PhotoClass photo = new PhotoClass(SaveImgToRoot(path));

            return photo;
        }



        string SaveImgToRoot(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                string name = System.IO.Path.GetFileName(path);
                string newPath = $@"Photos\{name}";
                if (!File.Exists(newPath))
                    File.Copy(path, newPath);
                return newPath;
            }
            return defaultPath;
        }


        public BitmapImage GetPhotoFromPath(string pathToPhoto)
        {
            if (string.IsNullOrEmpty(pathToPhoto)) return null;
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(pathToPhoto, UriKind.Absolute);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();

            image.Height.ToString();
            return image;
        }
    }
}
