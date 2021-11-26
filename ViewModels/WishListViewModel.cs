using DataClasses;
using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModels.Contract;

namespace ViewModels
{
    public class WishListViewModel: IWishListViewModel
    {
        IWishListWorkflow workflow;
        public WishListViewModel(IWishListWorkflow workfolw)
        {
            this.workflow = workfolw;
        }

        public event EventHandler ItemAdded;

        public void AddGame(string name, string konsole, string handler, float price)
        {
            workflow.AddWishListItem(name, konsole, handler, price);
            OnItemAdded();
        }

        public IEnumerable<WishListItem> GetItems(string selectedItem)
        {
            return workflow.WishList.Where(x => x.KonsoleType == selectedItem);
        }

        public IEnumerable<string> GetKonsolen()
        {
            var allConsoles = workflow.WishList.Select(x => x.KonsoleType);
            if(allConsoles.Count() != 0)
            {
                return allConsoles.Distinct();
            }
            return new List<string>();
        }

        public void RemoveGame(string iD)
        {
            workflow.RemoveWishListItem(iD);
            OnItemAdded();
        }

        private void OnItemAdded()
        {
            ItemAdded?.Invoke(this, new EventArgs());
        }
    }
}
