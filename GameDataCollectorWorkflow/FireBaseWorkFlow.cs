using DataClasses;
using DataManaging.Contract;
using Firebase.Database;
using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameDataCollectorWorkflow
{
    public class FireBaseWorkFlow : IFireBaseWorkFlow
    {
        private IFireBaseConnector connector;
        private IFireBaseDataHandler dataHandler;
        private IGameDataWorkflow data;
        private IWishListWorkflow wishList;
        private FirebaseClient client;
        private string clientID;
        public bool LoggedIn { get; private set; }
        public FireBaseWorkFlow(IFireBaseConnector connector, IFireBaseDataHandler dataHandler, IGameDataWorkflow data, IWishListWorkflow wishList)
        {
            this.connector = connector;
            this.dataHandler = dataHandler;
            this.data = data;
            this.wishList = wishList;
            LoggedIn = false;
        }
        public async Task LoadDataOnFireBase()
        {
            if(LoggedIn)
            {
                var objects = await dataHandler.LoadData(client, clientID);
                data.Games.Clear();
                data.Games.AddRange((List<Game>)objects[0]);

                data.Storages.Clear();
                data.Storages.AddRange((List<Storage>)objects[1]);

                data.Konsolen.Clear();
                data.Konsolen.AddRange((List<Konsole>)objects[2]);

                wishList.WishList.Clear();
                wishList.WishList.AddRange((List<WishListItem>)objects[3]);
            }
        }

        public void LogIn(string email, string passwort)
        {
            client = connector.LogIn(email, passwort);
            clientID = connector.GetClientID();
            LoggedIn = true;
        }

        public void LogOut()
        {
            connector.LogOut(client);
            client = null;
            LoggedIn = false;
        }

        public async Task SaveDataOnFireBase()
        {
            if (LoggedIn)
            {
                object[] objects = new object[]
                {
                data.Games,
                data.Storages,
                data.Konsolen,
                wishList.WishList
                };
                await dataHandler.SaveData(client, objects, clientID);
            }
        }

        public void Register(string email, string passwort)
        {
            connector.Register(email, passwort);
            clientID = connector.GetClientID();
            LoggedIn = true;
        }
    }
}
