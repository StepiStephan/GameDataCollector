using DataClasses;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseManagig
{
    public class FirebaseManager
    {
        public async Task SaveData(FirestoreDb db, List<Konsole> konsolen, List<Storage> storages, List<Game> games)
        {
            await SaveData(db, games);
            await SaveData(db, konsolen);
            await SaveData(db, storages);
        }

        private async Task SaveData<T>(FirestoreDb db, List<T> data)
        {
            var reference = db.Collection(typeof(T).Name).Document();
            await reference.SetAsync(data);
        }

        public async Task<FirestoreDb> ConnectToFirebase(string project)
        {
            return await FirestoreDb.CreateAsync(project);
        }

        public async Task<LoadedData> LoadData(FirestoreDb db)
        {
            var data = new LoadedData();

            data.Games = await LoadData<Game>(db);
            data.Storages = await LoadData<Storage>(db);
            data.Konsolen = await LoadData<Konsole>(db);

            return data;
        }

        private async Task<List<T>> LoadData<T>(FirestoreDb db)
        {
            var snapshot = await db.Collection(typeof(T).Name).GetSnapshotAsync();
            List<T> konsolen = new List<T>();
            foreach(var document in snapshot.Documents)
            {
                var konsole = document.ConvertTo<T>();
                konsolen.Add(konsole);
            }

            return konsolen;
        }
    }
}
