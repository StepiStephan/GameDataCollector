using DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHandling
{
    public class KonsoleHandler : IKonsoleHandler
    {
        private List<Konsole> consoles;

        public List<Konsole> Consoles { get => consoles; private set => consoles = value; }

        public void AddConsole(Konsole console)
        {
            Consoles.Add(console);
        }
        public Konsole GetConsole(string id)
        {
            var console = Consoles.Where(x => x.Id == id).FirstOrDefault();
            if (console != null)
            {
                return console;
            }
            else
            {
                return null;
            }
        }
    }
}
