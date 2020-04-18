using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WPF_MD_Personnel_Records
{
    public class Member : INotifyPropertyChanged, IDataErrorInfo
    {

        string FullInfo;
        private int Id;
        private string Name;
        private string Surname;
        private string Patronymic;
        private string PhoneNum;
        private string Note;
        private string Sex;
        private Position Position;
        private PhotoClass Photo;

        public Member(int id = 0, string name = null, string surname = null, string patronymic = null, string phoneNum = null, string sex = null, Position position = null, PhotoClass photo = null, string note = null):this()
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.patronymic = patronymic;
            this.phoneNum = phoneNum;
            this.sex = sex;
            this.position = position;
            this.photo = photo;
            this.note = note;
            fullInfo = $"{surname} {name.Substring(0, 1)}.{Patronymic.Substring(0, 1)}";
        }
        public Member()
        {
            errors = new Dictionary<string, string>();
            sex = "чол";
        }

       public bool IsAnyOfNeededFieldsIsNull()
        {
            if(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname) || string.IsNullOrWhiteSpace(Patronymic) || Position == null)
            {
                return true;
            }
            return false;
        }

        public Member GetCopy()
        {
            Member clone = new Member()
            {
                id = this.id,
                name = this.name,
                surname = this.surname,
                patronymic = this.patronymic,
                phoneNum = this.phoneNum,
                sex = this.sex,
                position = this.position,
                photo = this.photo,
                note = this.note
            };
            return clone;
        }


        public int id
        {
            get => Id;
            set
            {
                Id = value;
                Notify("id");
            }
        }

        public string name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
                if (string.IsNullOrWhiteSpace(value))
                {
                    errors["name"] = "Порожнє ім`я";
                }
                else
                {
                    errors["name"] = null;
                }
                Notify("name");
            }
        }

        public string surname
        {
            get => Surname;
            set
            {
                Surname = value;
                if (string.IsNullOrWhiteSpace(value))
                {
                    errors["surname"] = "Порожня фамілія";
                }
                else
                {
                    errors["surname"] = null;
                }
                Notify("surname");
            }
        }
        public string patronymic
        {
            get => Patronymic;
            set
            {
                Patronymic = value;
                if (string.IsNullOrWhiteSpace(value))
                {
                    errors["patronymic"] = "Порожнє по-батькові";
                }
                else
                {
                    errors["patronymic"] = null;
                }
                Notify("patronymic");
            }
        }
        public string phoneNum
        {
            get {
                if (string.IsNullOrEmpty(PhoneNum))
                    return "";
                return PhoneNum;
            }
            set
            {
                PhoneNum = value;
                Notify("phoneNum");
            }
        }
        public string note
        {
            get
            {
                if (string.IsNullOrEmpty(Note))
                    return "";
                return Note;
            }
            set
            {
                Note = value;
                if (string.IsNullOrEmpty(Note))
                    Note = "";
                Notify("note");
            }
        }
        public string sex
        {
            get => Sex;
            set
            {
                Sex = value;
                Notify("sex");
            }
        }
        public Position position
        {
            get => Position;
            set
            {
                Position = value;
                Notify("position");
            }
        }
        public PhotoClass photo
        {
            get => Photo;
            set
            {
                Photo = value;
                Notify("photo");
            }
        }

        public string fullInfo
        {
            get => FullInfo;
            set
            {
                FullInfo = value;
                Notify("fullInfo");
            }
        }

        public List<Member> GetMembers()
        {
            List<Member> list = new SqlManager().GetAllMembers();

            return list;
        }

        #region INotifyPropertychanged
        public event PropertyChangedEventHandler PropertyChanged;

        void Notify(string parameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(parameter));
            }
        }
        #endregion

        #region IDataErrorInfo
        public Dictionary<string, string> errors;

        public string this[string columnName] => errors.ContainsKey(columnName)?errors[columnName]:null;

        public string Error => throw new NotImplementedException();
        #endregion

    }
}
