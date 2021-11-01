namespace ViewModels.Contract.DataClasses
{
    public class InfoClass
    {
        public string Name { get; }
        public float Space { get; }
        public string SavePoint { get; }

        public InfoClass(string name, float space, string position)
        {
            Name = name;
            Space = space;
            SavePoint = position;
        }

        public override string ToString()
        {
            return Name + " -> " + SavePoint;
        }
    }
}