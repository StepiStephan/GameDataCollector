using System;

namespace DataManaging
{
    public abstract class DataSaverLoaderBase<T>
    {
        public string PathToSave { get; }
        public DataSaverLoaderBase()
        {
            PathToSave = Environment.CurrentDirectory;
        }
    }
}
