using System;
using System.Collections.Generic;
using Enums;

namespace DataClasses
{
    public class Game
    {
        private string name;
        private List<Genre> gameGenres;
        private float spaceOnSorage;
        private string id;

        public string Name { get => name; set => name = value; }
        public float SpaceOnSorage { get => spaceOnSorage; set => spaceOnSorage = value; }
        public string Id { get => id; set => id = value; }
        public List<Genre> GameGenre { get => gameGenres; set => gameGenres = value; }

        public Game()
        {

        }
    }
}