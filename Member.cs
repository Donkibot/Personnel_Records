using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_MD_Personnel_Records
{
    class Member
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public string surname { get; private set; }
        public string patronymic { get; private set; }
        public string phoneNum { get; private set; }
        public string note { get; private set; }
        public string sex { get; private set; }
        public Position position;
        public DB_Image photo;

        public Member(int id, string name, string surname, string patronymic, string phoneNum, string sex, Position position, DB_Image photo, string note)
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
        }
    }
}
