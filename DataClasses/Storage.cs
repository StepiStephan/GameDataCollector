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

        public float Space { get => space; set => space = value; }
        public string Name { get => name; set => name = value; }
        public string Id { get => id; set => id = value; }
        public List<string> Games { get => games; set => games = value; }

        public Storage()
        {

        }
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

        public Storage Copy()
        {
            var result = new Storage(Space, Name, Id);

            foreach(var gameID in Games)
            {
                result.AddGame(gameID);
            }

            return result;
        }

        public override string ToString()
        {
            string result = "Name: " + name + "; Speicherplatz: " + space + "; Games: \n\r";
            return result;
        }
    }
}