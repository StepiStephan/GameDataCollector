using System;
using System.Collections.Generic;

namespace DataClasses
{
    public class Konsole
    {
        public string ConsoleName { get; set; }
        public string Name { get; set;}
        public string Id { get; set; }
        public List<string> Storages { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
