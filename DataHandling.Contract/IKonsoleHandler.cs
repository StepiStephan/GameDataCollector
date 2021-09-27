using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataClasses
{
    public interface IKonsoleHandler
    {
        void RanameKonsole(string name);
        Storage GetStorage(string id);
        void AddStorage(Storage storage);
    }
}
