using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WPF_MD_Personnel_Records
{
    class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public bool canEdit
        {
            get { return _canEdit; }
            set
            {
                _canEdit = value;
                Notify("canEdit");
            }
        }
        private bool _canEdit = true;
    }
}
