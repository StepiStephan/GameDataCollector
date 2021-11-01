using System;
using System.Collections.Generic;
using System.Text;
using ViewModels.Contract.DataClasses;

namespace ViewModels.Contract
{
    public interface ISearchViewModel
    {

        IEnumerable<InfoClass> GetMatchingGame(string newTextValue);
    }
}
