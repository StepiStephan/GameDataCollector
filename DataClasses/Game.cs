using System.Collections.Generic;

namespace DataClasses
{
    public class Game
    {
        private string name;
        private List<Genre> gameGenres;
        private float spaceOnSorage;
        private string id;

        public string Name { get => name; }
        public float SpaceOnSorage { get => spaceOnSorage; }
        public string Id { get => id; }
        public List<Genre> GameGenre { get => gameGenres; }

        public Game(string name, List<Genre> gameGenres, float spaceOnSorage, string id)
        {
            this.name = name;
            this.gameGenres = gameGenres;
            this.spaceOnSorage = spaceOnSorage;
            this.id = id;
        }
    }
}