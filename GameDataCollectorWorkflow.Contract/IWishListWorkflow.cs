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
        void AddWishListItem(string name, string konsoleType, string store, float amount);
        void RemoveWishListItem(string id);
        void DuplicateForAnoherKonsole(string id, string konsole);
    }
}
