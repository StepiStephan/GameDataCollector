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

        public Konsole(string consoleName, string name, float spaceIntern)
        {
            this.consoleName = consoleName;
            this.name = name;
            this.id = Guid.NewGuid().ToString();
            this.storages = new List<Storage>()
            {
                new Storage(spaceIntern, "InternerSpeicher")
            };
        }

        public void AddStorage(Storage storage)
        {
            if (storage != null)
            {
                storages.Add(storage);
            }
        }

        public override string ToString()
        {
            string result = "Name: " + name + "; ConsolenName: " + consoleName + "; ID: " + id + "; Festplatten: "+ Environment.NewLine;
            foreach(var storage in storages)
            {
                result += storage.ToString() + " ; " + Environment.NewLine;
            }
            return result;
        }
    }
}
