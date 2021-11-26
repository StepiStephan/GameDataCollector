using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataManaging.Contract
{
    public interface IFireBaseDataHandler
    {
        Task SaveData(FirebaseClient client, object[] objects, string clientID);
        Task<object[]> LoadData(FirebaseClient client, string clientID);
    }
}
