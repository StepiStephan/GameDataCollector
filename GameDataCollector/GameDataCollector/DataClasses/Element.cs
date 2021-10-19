using System;
using System.Collections.Generic;
using System.Text;

namespace GameDataCollector.DataClasses
{
    public class Element
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
