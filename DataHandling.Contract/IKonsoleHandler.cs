using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataClasses
{
    public interface IKonsoleHandler
    {
        void RanameKonsole(string id, string name);
        Konsole GetKonsole(string name, string consloeName);
        void AddStorage()
    }
}
