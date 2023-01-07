using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses
{
    public class TableContent
    {
        public List<string> Names { get; set; } = new List<string>();
        public List<float> Pice1 { get; set; } = new List<float>();
        public List<float> Pice2 { get; set; } = new List<float>();
        public List<float> Pice3 { get; set; } = new List<float>();
        public List<DateTime> ReleaseDate { get; set; } = new List<DateTime>();
        public List<string> ConsoleType { get; set; } = new List<string>();
        public List<float> Günstigster { get; set; } = new List<float>();
    }
}
