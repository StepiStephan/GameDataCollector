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

        public Storage(float space, string name, string id, List<Game> games)
        {
            this.space = space;
            this.name = name;
            this.id = id;
            this.games = games;
        }
    }
}