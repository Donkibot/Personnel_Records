using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_MD_Personnel_Records
{
    class Position
    {
        public int id { get; private set;}
        public string position { get; private set;}

        public Position(int _id, string _position)
        {
            id = _id;
            position = _position;
        }
    }
}
