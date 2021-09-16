using System;
using System.Collections.Generic;

namespace DataClasses
{
    public class Konsole
    {
        private string consoleName;
        private string name;
        private string id;
        private List<Storage> storages;

        public string ConsoleName { get => consoleName; }
        public string Name { get => name; }
        public string Id { get => id; }
        public List<Storage> Storages { get => storages; }

        public Konsole(string consoleName, string name, string id, List<Storage> storages)
        {
            this.consoleName = consoleName;
            this.name = name;
            this.id = id;
            this.storages = storages;
        }
    }
}
