using System;
using System.IO;
using System.Threading.Tasks;
using DataManaging.Contract;
using Newtonsoft.Json;

namespace DataManaging
{
    public class DataSaver<T> : DataSaverLoaderBase<T>, IDataSaver<T>
    {
        private string path;
        private string name;
        private bool tryedBackupPath = false;
        public void SaveObject(T data)
        {
            try
            {
                var dataString = JsonConvert.SerializeObject(data);
                File.WriteAllText(path, dataString);
                tryedBackupPath = false;
            }
            catch (IOException)
            {
                Task.Delay(500).GetAwaiter().GetResult();
                SaveObject(data);
            }
            catch(UnauthorizedAccessException)
            {
                if (!tryedBackupPath)
                {
                    SetBackupPath();
                    SetName(name);
                    if(!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                    {
                        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                    }
                    SaveObject(data);
                    tryedBackupPath = true;
                }
                else
                {
                    throw;
                }
            }


        }

        public void SetName(string name)
        {
            this.name = name;
            path = Path.Combine(PathToSave, name + ".txt");
        }
    }
}
