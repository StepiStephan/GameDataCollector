using System;
using System.Collections.Generic;

namespace DataClasses
{
    public class Konsole
    {
        private string consoleName;
        private string name;
        private string id;
        private List<string> storages;

        public string ConsoleName { get => consoleName; set => consoleName = value; }
        public string Name { get => name; set => name = value;}
        public string Id { get => id; set => id = value; }
        public List<string> Storages { get => storages; set => storages = value; }

        public Konsole()
        {

        }
        public Konsole(string consoleName, string name)
        {
            var id = Guid.NewGuid().ToString();
            Initialze(consoleName, name, id);
        }
        public Konsole(string consoleName, string name, string id)
        {
            Initialze(consoleName, name, id);
        }
        private void Initialze(string consoleName, string name, string id)
        {
            this.consoleName = consoleName;
            this.name = name;
            this.id = id;
            this.storages = new List<string>();
        }

        public void AddStorage(string storageId)
        {
            if (storageId != null)
            {
                storages.Add(storageId);
            }
        }

        public Konsole Copy()
        {
            var result = new Konsole(ConsoleName, Name, Id);
            
            foreach(var storageId in Storages)
            {
                result.AddStorage(storageId);
            }

            return result;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
