using System;
using System.Collections.Generic;
using System.Text;

namespace DataClasses
{
    public class WishListItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string KonsoleType { get; set; }
        public string Store { get; set; }
        public float Ammount { get; set; }
        public List<(string ShopName, float Price)> Anbieter { get; set; } = new List<(string, float)>();

    }
}
