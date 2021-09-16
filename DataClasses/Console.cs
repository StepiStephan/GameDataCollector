using System;
using System.Collections.Generic;

namespace DataClasses
{
    public class Console
    {
        private string consoleName;
        private string name;
        private int id;
        private List<Storage> storages;

        public string ConsoleName { get => consoleName; }
        public string Name { get => name; }
        public int Id { get => id; }
        public List<Storage> Storages { get => storages; }

        public Console(string consoleName, string name, int id, List<Storage> storages)
        {
            this.consoleName = consoleName;
            this.name = name;
            this.id = id;
            this.storages = storages;
        }
    }
}
