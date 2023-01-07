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
        void AddGame(string name, string konsole, List<(string, float)> angebote, DateTime releaseDate);
        void RemoveGame(string iD);
        List<WishListItem> ImportTableClass(string path);
        void ExportTableClass(string path, List<WishListItem> table);
    }
}
