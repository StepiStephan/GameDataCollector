using DataClasses;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirebaseManaging.Contract
{
    public interface IFirebaseManager
    {
        Task SaveData(FirestoreDb db, List<Konsole> konsolen, List<Storage> storages, List<Game> games);
    }
}
