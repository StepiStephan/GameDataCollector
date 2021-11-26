using DataClasses;
using DataManaging.Contract;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataManaging
{
    public class FireBaseDataHandler: IFireBaseDataHandler
    {
        private const string gameChild = "Games";
        private const string storageChild = "Storages";
        private const string konsoleChild = "Konsolen";
        private const string wishListChild = "WishList";
        public async Task SaveData(FirebaseClient client, object[] objects, string clientID)
        {
            var games = (List<Game>)objects[0];
            var storages = (List<Storage>)objects[1];
            var konsolen = (List<Konsole>)objects[2];
            var wishList = (List<WishListItem>)objects[3];

            await SaveData<Game>(client, games, gameChild, clientID);
            await SaveData<Storage>(client, storages, storageChild, clientID);
            await SaveData <Konsole>(client, konsolen, konsoleChild, clientID);
            await SaveData <WishListItem>(client, wishList, wishListChild, clientID);
        }

        private async Task SaveData<T>(FirebaseClient client, List<T> objectList, string name, string clientID)
        {
            await client.Child(name).DeleteAsync();
            await client.Child(name).PostAsync(objectList);
        }

        public async Task<object[]> LoadData(FirebaseClient client, string clientID)
        {
            var games = await GetData<Game>(client, gameChild, clientID);
            var storages = await GetData<Storage>(client, storageChild,clientID);
            var konsolen = await GetData<Konsole>(client, konsoleChild, clientID);
            var wishList = await GetData<WishListItem>(client, wishListChild, clientID);

            object[] objects = new object[]
            {
                games,
                storages,
                konsolen,
                wishList
            };

            return objects;
        }

        private async Task<List<T>> GetData<T>(FirebaseClient client, string name, string clientID)
        {
            var objectDatabase = await client.Child(clientID).Child(name).OnceAsync<T>();
            List<T> objects = new List<T>();

            foreach (var objectClass in objectDatabase)
            {
                objects.Add(objectClass.Object);
            }

            return objects;
        }
    }
}
