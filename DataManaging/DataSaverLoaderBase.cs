using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
