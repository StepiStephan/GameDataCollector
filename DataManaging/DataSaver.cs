using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataManaging.Contract;
using Newtonsoft.Json;

namespace DataManaging
{
    public class DataSaver<T> : DataSaverLoaderBase<T>, IDataSaver<T>
    {
        public DataSaver() : base()
        {

        }
        public void SaveObject(T data)
        {
            var dataString = JsonConvert.SerializeObject(data);
            if (!File.Exists(PathToSave))
            {
                File.Create(PathToSave);
            }
            File.WriteAllText(PathToSave, dataString);
        }
    }
}
