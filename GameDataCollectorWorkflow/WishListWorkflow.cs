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
        public List<WishListItem> WishList => wishList;
        private const string saveName = "WishListData";
        List<WishListItem> wishList;
        IDataSaver<List<WishListItem>> dataSaver;
        IDataLoader<List<WishListItem>> dataLoader;

        public event EventHandler LsitChange;

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
            LsitChange?.Invoke(this, new EventArgs());
        }
    }
}
