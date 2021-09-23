using System;
using System.Collections.Generic;

namespace DataClasses
{
    public class Storage
    {
        private float space;
        private string name;
        private string id;
        private List<Game> games;

        public float Space { get => space; }
        public string Name { get => name; }
        public string Id { get => id; }
        public List<Game> Games { get => games; }

        public Storage(float space, string name)
        {
            this.space = space;
            this.name = name;
            this.id = Guid.NewGuid().ToString();
            this.games = new List<Game>();
        }

        public void AddGame(Game game)
        {
            if(game != null)
            {
                games.Add(game);
            }
        }

        public override string ToString()
        {
            string result = "Name: " + name + "; ID: " + id + "; Speicherplatz: " + space + "; Games: "+ Environment.NewLine;
            foreach(var game in games)
            {
                result += game.ToString() + Environment.NewLine;
            }
            return result;
        }
    }
}