using DataManaging.Contract;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataManaging
{
    public class FireBaseConnector : IFireBaseConnector
    {
        private const string secretKey = "AIzaSyCOqADfqBQS8EZUyoC3qmUQrAGuAhAJMks";
        private const string database = "gamedatacollector-2eb2f-default-rtdb";
        private const string databaseURL = "https://gamedatacollector-2eb2f-default-rtdb.firebaseio.com/";
        private string clientID;

        public string GetClientID()
        {
            return clientID.Replace("@", "").Replace(".", "");
        }

        public FirebaseClient LogIn(string email, string passwort)
        {
            var firebaseToken = LogInAsync(email, passwort);

            var firebaseClient = new FirebaseClient(databaseURL, 
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(firebaseToken)
                });

            
            return firebaseClient;
        }

        public void LogOut(FirebaseClient client)
        {
            client.Dispose();
        }

        private string LogInAsync(string email, string passwort)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(secretKey));
            var auth = authProvider.SignInWithEmailAndPasswordAsync(email, passwort).Result;
            clientID = auth.User.Email;

            return auth.FirebaseToken;
        }

        public FirebaseClient Register(string email, string passwort)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(secretKey));
            var auth = authProvider.CreateUserWithEmailAndPasswordAsync(email, passwort).Result;

            clientID = auth.User.Email;

            var firebaseClient = new FirebaseClient(databaseURL,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(clientID)
                });

            return firebaseClient;
        }
    }
}
