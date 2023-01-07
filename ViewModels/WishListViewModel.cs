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

        public void AddGame(string name, string konsole, List<(string, float)> angebote, DateTime releaseDate)
        {
            workflow.AddWishListItem(name, konsole, angebote, releaseDate);
            OnItemAdded();
        }

        public void ExportTableClass(string path, List<WishListItem> table)
        {
            var tableClass = new TableClass();

            if (table.Count != 0)
            {
                tableClass.TableName = table.First().KonsoleType;

                tableClass.ColumnCaptions.AddRange(new List<string>() { "Name", "Konsole" ,"Erscheinungsdatum" });
                tableClass.ColumnCaptions.AddRange(table.First().Anbieter.Select(x => x.ShopName));
                tableClass.ColumnCaptions.Add("Günstigster Preis");

                var content = new TableContent();
                content.ReleaseDate = table.Select(x=> x.ReleaseDate).ToList();
                content.Names = table.Select(x => x.Name).ToList();

                content.Pice1 = table.Select(x => x.Anbieter[0].Price).ToList();
                content.Pice2 = table.Select(x => x.Anbieter[1].Price).ToList();
                content.Pice3 = table.Select(x => x.Anbieter[2].Price).ToList();
                content.ConsoleType = table.Select(x => x.KonsoleType).ToList();

                content.Günstigster = table.Select(x => x.Ammount).ToList();
                tableClass.Content = content;

                workflow.ExportTable(tableClass, path);
            }
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

        public List<WishListItem> ImportTableClass(string path)
        {
            List<WishListItem> tableList = new List<WishListItem>();
            var table = workflow.ImportTable(path);
            for(int i = 0; i < table.Content.Names.Count; i++) 
            {
                var newItem = new WishListItem();
                newItem.Name = table.Content.Names[i];
                newItem.ReleaseDate = table.Content.ReleaseDate[i];
                newItem.ID = Guid.NewGuid().ToString();

                newItem.Anbieter.Add(GetAnbieterValue(0, table, i));
                newItem.Anbieter.Add(GetAnbieterValue(1, table, i));
                newItem.Anbieter.Add(GetAnbieterValue(2, table, i));

                var cheapest = newItem.Anbieter.Where(x=> x.Price != 0).Min(x => x.Price);
                newItem.Ammount = cheapest;
                newItem.Store = newItem.Anbieter.Where(x => x.Price == cheapest).Select(x => x.ShopName).First();
                var name = table.Content.ConsoleType.Count == 0 ?  table.TableName : table.Content.ConsoleType[i];
                if (name.StartsWith("Spiele") && name.Length > 11)
                {
                    name = name.Substring(10);
                }
                newItem.KonsoleType = name.Trim();
                tableList.Add(newItem);
            }

            return tableList;
        }

        private (string, float) GetAnbieterValue(int priceCount, TableClass table, int i)
        {
            var index = 2;
            if(table.ColumnCaptions.Count > 5 )
            {
                index++;
            }
            var caption = table.ColumnCaptions[index + priceCount];
            var anbieterName = string.Empty;
            if (caption.Contains('('))
            { 
                anbieterName = caption.Split('(')[1].Split(')')[0];
            }
            else
            {
                anbieterName = caption.Replace("Preis ", "");
            }
            float price = 0;

            switch(priceCount)
            {
                case 0:
                    price = table.Content.Pice1[i]; 
                    break;

                case 1:
                    price = table.Content.Pice2[i];
                    break;

                case 2:
                    price = table.Content.Pice3[i];
                    break;
            }
            
            return (anbieterName, price);
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
