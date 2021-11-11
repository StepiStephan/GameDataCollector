using System;

namespace DataManaging
{
    public abstract class DataSaverLoaderBase<T>
    {
        public string PathToSave { get; private set; }
        public DataSaverLoaderBase()
        {
            PathToSave = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
        }

        protected void SetBackupPath()
        {
            PathToSave = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "GameDataCollector", "SaveData");
        }
    }
}
