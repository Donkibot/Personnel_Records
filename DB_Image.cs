using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPF_MD_Personnel_Records
{
    class DB_Image
    {
        public int id { get; private set; }
        public byte[] data { get; private set; }
        public string format { get; private set; }

        public DB_Image(int id, byte[] data, string format)
        {
            this.id = id;
            this.data = data;
            this.format = format;
        }

        public BitmapImage GetPhoto()
        {
            string tempPath = $"Photos/{id}{format}";
            BitmapImage image = new BitmapImage();
            using (FileStream fs = new FileStream(tempPath, FileMode.OpenOrCreate))
            {
                fs.Write(data, 0, data.Length);
                
            }

            image.BeginInit();
            image.UriSource = new Uri(tempPath, UriKind.Relative);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();

            image.Height.ToString();
                return image;
        }
    }
}
