using System;
using System.Collections.Generic;

namespace DataClasses
{
    public class Storage
    {
        public float Space { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public List<string> Games { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}