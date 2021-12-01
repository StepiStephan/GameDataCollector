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

        public event EventHandler DatabaseLoaded;
        public event EventHandler DatabaseSaved;

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

                if (((List<List<Konsole>>)objects[2]).Count > 0)
                {
                    data.Konsolen.Clear();
                    var konsolenListe = ((List<List<Konsole>>)objects[2]);
                    data.Konsolen = konsolenListe[0];
                }

                if (((List<List<Storage>>)objects[1]).Count > 0)
                {
                    data.Storages.Clear();
                    var storagesList = (List<List<Storage>>)objects[1];
                    data.Storages = storagesList[0];
                }

                if (((List<List<Game>>)objects[0]).Count != 0)
                {
                    data.Games.Clear();
                    var gameList = ((List<List<Game>>)objects[0]);
                    data.Games = gameList[0];
                }

                if (((List<List<WishListItem>>)objects[3]).Count > 0)
                {
                    wishList.WishList.Clear();
                    var wishListList = ((List<List<WishListItem>>)objects[3]);
                    wishList.WishList = wishListList[0];
                }
                OnDatabaseLoaded();
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
                OnDatabaseSaved();
            }
        }

        public void Register(string email, string passwort)
        {
            connector.Register(email, passwort);
            clientID = connector.GetClientID();
            LoggedIn = true;
        }

        private void OnDatabaseLoaded()
        {
            DatabaseLoaded?.Invoke(dataHandler, new EventArgs());
        }

        private void OnDatabaseSaved()
        {
            DatabaseSaved?.Invoke(dataHandler, new EventArgs());
        }
    }
}
