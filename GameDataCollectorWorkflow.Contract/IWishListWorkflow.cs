using DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDataCollectorWorkflow.Contract
{
    public interface IWishListWorkflow
    {
        event EventHandler ListChange;
        List<WishListItem> WishList { get; set; }
        void AddWishListItem(string name, string konsoleType, List<(string store, float amount)> angebote, DateTime releaseDate);
        void RemoveWishListItem(string id);
        void DuplicateForAnoherKonsole(string id, string konsole);
        void ExportTable(TableClass tableClass , string path);
        TableClass ImportTable(string path);
    }
}
