using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Contract
{
    public interface IFireBaseViewModel
    {
        bool LoggedIn { get; }

        event EventHandler LoggedInStateChanged;
        void LogIn(string email, string passwort);
        void LogOut();
        Task SaveData();
        Task LoadData();
        void Register(string email, string passwort);
    }
}
