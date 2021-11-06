using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Contract
{
    public interface IStartPageViewModel
    {
        int GetKonsolenCount();
        int GetStorageCount();
        int GetGameCount();
    }
}
