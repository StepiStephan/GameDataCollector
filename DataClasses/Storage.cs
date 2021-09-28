using System;
using System.Collections.Generic;

namespace DataClasses
{
    public class Storage
    {
        private float space;
        private string name;
        private string id;
        private List<string> games;

        public float Space { get => space; }
        public string Name { get => name; }
        public string Id { get => id; }
        public List<string> Games { get => games; }

        public Storage(float space, string name)
        {
            var id = Guid.NewGuid().ToString();
            Initialzie(space, name, id);
            
        }
        public Storage(float space, string name, string id)
        {
            Initialzie(space, name, id);
        }
        private void Initialzie(float space, string name, string id)
        {
            this.space = space;
            this.name = name;
            this.id = id;
            this.games = new List<string>();
        }

        public void AddGame(string gameId)
        {
            if(gameId != null)
            {
                games.Add(gameId);
            }
        }

        public override string ToString()
        {
            string result = "Name: " + name + "; Speicherplatz: " + space + "; Games: \n\r";
            return result;
        }
    }
}