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
        public DataLoader() : base()
        {

        }

        public T LoadObject()
        {
            string dataString = string.Empty;
            if (File.Exists(PathToSave))
            {
                dataString = File.ReadAllText(PathToSave);
            }

            return JsonConvert.DeserializeObject<T>(dataString);
        }
    }
}
