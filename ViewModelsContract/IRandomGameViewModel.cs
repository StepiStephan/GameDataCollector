using DataClasses;
using Enums;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels.Contract.DataClasses;

namespace ViewModels.Contract
{
    public interface IRandomGameViewModel
    {
        List<InfoClass> GetGames(IEnumerable<Genre> genres, IEnumerable<Descriptor> descriptors);
    }
}
