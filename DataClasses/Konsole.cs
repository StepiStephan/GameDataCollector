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

        public string ConsoleName { get => consoleName; }
        public string Name { get => name; }
        public string Id { get => id; }
        public List<string> Storages { get => storages; }

        public Konsole(string consoleName, string name, float spaceIntern, string interneStorageId)
        {
            this.consoleName = consoleName;
            this.name = name;
            this.id = Guid.NewGuid().ToString();

            this.storages = new List<string>()
            {
                interneStorageId
            };
        }
        public Konsole(string consoleName, string name, float spaceIntern, string interneStorageId, string id)
        {
            this.consoleName = consoleName;
            this.name = name;
            this.id = id;
            this.storages = new List<string>()
            {
                interneStorageId
            };
        }

        public void AddStorage(string storageId)
        {
            if (storageId != null)
            {
                storages.Add(storageId);
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
