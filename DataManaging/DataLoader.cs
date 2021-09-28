using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataManaging.Contract;
using Newtonsoft.Json;

namespace DataManaging
{
    public class DataLoader<T> : DataSaverLoaderBase<T>, IDataLoader<T>
    {
        private readonly string path;
        public DataLoader(string name) : base()
        {
            path = Path.Combine(PathToSave, name + ".txt");
        }
        
        public T LoadObject()
        {
            string dataString = string.Empty;
            if (File.Exists(path))
            {
                dataString = File.ReadAllText(path);
            }

            return JsonConvert.DeserializeObject<T>(dataString);
        }
    }
}
