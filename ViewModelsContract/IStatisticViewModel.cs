using System;
using System.Collections.Generic;
using System.Text;
using ViewModels.Contract.DataClasses;

namespace ViewModels.Contract
{
    public interface IStatisticViewModel
    {
        List<InfoClass> Data { get; }
        string SelectedElementName { get; }
    }
}
