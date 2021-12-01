using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameDataCollectorWorkflow.Contract
{
    public interface IFireBaseWorkFlow
    {
        event EventHandler DatabaseLoaded;
        event EventHandler DatabaseSaved;
        bool LoggedIn { get; }
        void LogIn(string email, string passwort);
        void LogOut();
        Task SaveDataOnFireBase();
        Task LoadDataOnFireBase();
        void Register(string email, string passwort);
    }
}
