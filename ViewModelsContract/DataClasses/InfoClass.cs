namespace ViewModels.Contract.DataClasses
{
    public class InfoClass
    {
        public string Name { get; }
        public float Space { get; }

        public InfoClass(string name, float space)
        {
            Name = name;
            Space = space;
        }
    }
}