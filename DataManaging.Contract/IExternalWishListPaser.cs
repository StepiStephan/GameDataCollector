using DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataManaging.Contract
{
    public interface IExternalWishListPaser
    {
        TableClass ParseCsv(string path);
        void ParseTable(TableClass table, string path);
    }
}
