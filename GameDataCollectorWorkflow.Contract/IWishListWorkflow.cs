using DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDataCollectorWorkflow.Contract
{
    public interface IWishListWorkflow
    {
        event EventHandler LsitChange;
        List<WishListItem> WishList { get; }
        void AddWishListItem(string name, string konsoleType, string store, float amount);
        void RemoveWishListItem(string id);
        void DuplicateForAnoherKonsole(string id, string konsole);
    }
}
