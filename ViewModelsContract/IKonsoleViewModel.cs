using DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Contract
{
    public interface IKonsoleViewModel
    {
        List<Konsole> Konsoles { get; }
        Konsole GetKonsole(string konsoleId);
        void EditKonsole(string konsoleId, string name, string consoleName);
        void DeleteKonsole(string id);
        void DeleteKonsoleWithAllGames(string id);
        void DeleteKonsoleWithAllStorages(string id);
        Konsole CreateKonsole(string konsoleName, string name, float internerSpeicher);
        void AddStorage(string konsoleId, Storage storage);
        void SetKonsole(string id);
        string GetInfo(Konsole konsole);
    }
}
