using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DataManaging.Contract;
using Newtonsoft.Json;

namespace DataManaging
{
    public class DataSaver<T> : DataSaverLoaderBase<T>, IDataSaver<T>
    {
        private readonly string path;
        public DataSaver(string name) : base()
        {
            path = Path.Combine(PathToSave, name + ".txt");
        }
        public void SaveObject(T data)
        {
            try
            {
                var dataString = JsonConvert.SerializeObject(data);
                File.WriteAllText(path, dataString);
            }
            catch(IOException)
            {
                Task.Delay(500).GetAwaiter().GetResult();
                SaveObject(data);
            }
        }
    }
}
