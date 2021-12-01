using DataClasses;
using DataManaging.Contract;
using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDataCollectorWorkflow
{
    public class WishListWorkflow : IWishListWorkflow
    {
        public List<WishListItem> WishList
        {
            get => wishList;
            set => wishList = value;
        }
        private const string saveName = "WishListData";
        private List<WishListItem> wishList;
        private IDataSaver<List<WishListItem>> dataSaver;
        private IDataLoader<List<WishListItem>> dataLoader;
        private IFireBaseWorkFlow fireBaseWorkFlow;

        public event EventHandler ListChange;

        public WishListWorkflow(IDataSaver<List<WishListItem>> dataSaver, IDataLoader<List<WishListItem>> dataLoader)
        {
            this.dataSaver = dataSaver;
            this.dataSaver.SetName(saveName);
            this.dataLoader = dataLoader;
            this.dataLoader.SetName(saveName);

            wishList = dataLoader.LoadObject();
            if(wishList == null)
            {
                wishList = new List<WishListItem>();
            }
        }

        public void SetIFirebaseWorkflow(IFireBaseWorkFlow fireBaseWorkFlow)
        {
            this.fireBaseWorkFlow = fireBaseWorkFlow;
            fireBaseWorkFlow.DatabaseLoaded += FireBaseWorkFlow_DatabaseLoaded;
            fireBaseWorkFlow.DatabaseSaved += FireBaseWorkFlow_DatabaseSaved;
        }

        private void FireBaseWorkFlow_DatabaseSaved(object sender, EventArgs e)
        {
            ListChange?.Invoke(fireBaseWorkFlow, new EventArgs());
        }

        private void FireBaseWorkFlow_DatabaseLoaded(object sender, EventArgs e)
        {
            ListChange?.Invoke(fireBaseWorkFlow, new EventArgs());
        }

        public void AddWishListItem(string name, string konsoleType, string store, float amount)
        {
            var wishListItem = new WishListItem()
            {
                ID = Guid.NewGuid().ToString(),
                Name = name,
                KonsoleType = konsoleType,
                Store = store,
                Ammount = amount
            };
            AddItem(wishListItem);
        }

        public void DuplicateForAnoherKonsole(string id, string konsole)
        {
            var item = wishList.Where(x => x.ID == id).First();

            var itemDuplicate = new WishListItem()
            {
                ID = Guid.NewGuid().ToString(),
                Name = item.Name,
                Store = item.Store,
                Ammount = item.Ammount,
                KonsoleType = konsole
            };

            AddItem(itemDuplicate);
        }

        public void RemoveWishListItem(string id)
        {
            wishList.Remove(wishList.Where(x => x.ID == id).FirstOrDefault());
            SaveData();
        }

        private void AddItem(WishListItem item)
        {
            wishList.Add(item);
            SaveData();
        }
        private void SaveData()
        {
            dataSaver.SaveObject(wishList);
            ListChange?.Invoke(this, new EventArgs());
        }
    }
}
