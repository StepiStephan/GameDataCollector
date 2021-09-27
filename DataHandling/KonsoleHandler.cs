using DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHandling
{
    public class KonsoleHandler : IKonsoleHandler
    {
        private Konsole consoles;

        public void AddStorage(Storage storage)
        {
            consoles.AddStorage(storage);
        }

        public Storage GetStorage(string id)
        {
            var result = consoles.Storages.Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        public void RanameKonsole(string name)
        {
            Konsole console = new Konsole(consoles.Name, name, consoles.Storages.First().Space, consoles.Id);
            consoles = console;
        }
    }
}
