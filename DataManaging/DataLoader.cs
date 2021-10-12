using System.IO;
using DataManaging.Contract;
using Newtonsoft.Json;

namespace DataManaging
{
    public class DataLoader<T> : DataSaverLoaderBase<T>, IDataLoader<T>
    {
        private string path;
        
        public T LoadObject()
        {
            string dataString = string.Empty;
            if (File.Exists(path))
            {
                dataString = File.ReadAllText(path);
            }

            return JsonConvert.DeserializeObject<T>(dataString);
        }

        public void SetName(string name)
        {
            path = Path.Combine(PathToSave, name + ".txt");
        }
    }
}
