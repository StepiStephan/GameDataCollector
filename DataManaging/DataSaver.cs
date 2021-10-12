using System.IO;
using System.Threading.Tasks;
using DataManaging.Contract;
using Newtonsoft.Json;

namespace DataManaging
{
    public class DataSaver<T> : DataSaverLoaderBase<T>, IDataSaver<T>
    {
        private string path;
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

        public void SetName(string name)
        {
            path = Path.Combine(PathToSave, name + ".txt");
        }
    }
}
