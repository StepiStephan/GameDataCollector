using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataManaging.Contract
{
    public interface IFireBaseConnector
    {
        FirebaseClient LogIn(string email, string passwort);
        void LogOut(FirebaseClient client);
        string GetClientID();
        FirebaseClient Register(string email, string passwort);
    }
}
