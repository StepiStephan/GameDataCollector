using DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Contract
{
    public interface IWishListViewModel
    {
        event EventHandler ItemAdded;
        IEnumerable<string> GetKonsolen();
        IEnumerable<WishListItem> GetItems(string selectedItem);
        void AddGame(string name, string konsole, string handler, float price);
        void RemoveGame(string iD);
    }
}
