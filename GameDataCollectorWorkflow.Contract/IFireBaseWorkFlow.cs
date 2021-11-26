using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameDataCollectorWorkflow.Contract
{
    public interface IFireBaseWorkFlow
    {
        bool LoggedIn { get; }
        void LogIn(string email, string passwort);
        void LogOut();
        Task SaveDataOnFireBase();
        Task LoadDataOnFireBase();
        void Register(string email, string passwort);
    }
}
