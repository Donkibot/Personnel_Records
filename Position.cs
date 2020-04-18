using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WPF_MD_Personnel_Records
{
    public class Position : INotifyPropertyChanged
    {

        int Id;
        string PositionField;
        public Position(int _id = 0, string _position = null)
        {
            id = _id;
            position = _position;
        }


        public List<Position> GetPositionsListFromDB()
        {
            List<Position> positions = new SqlManager().GetAllPosition();
            return positions;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void Notify(string parameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(parameter));
            }
        }



        public int id { 
            get => Id; 
            set {
                Id = value;
                Notify("id");
        }
        }
        public string position { 
            get => PositionField; 
            set {
                PositionField = value;
                Notify("position");
            }
        }


       
    }
}
