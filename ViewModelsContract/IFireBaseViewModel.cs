﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Contract
{
    public interface IFireBaseViewModel
    {
        bool LoggedIn { get; }

        event EventHandler LoggedInStateChanged;
        event EventHandler DatabaseLoaded;
        event EventHandler DatabaseSaved;
        void LogIn(string email, string passwort);
        void LogOut();
        Task SaveData();
        Task LoadData();
        void Register(string email, string passwort);
    }
}
