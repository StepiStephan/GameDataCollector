using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Contract;

namespace ViewModels
{
    public class FireBaseViewModel : IFireBaseViewModel
    {
        private IFireBaseWorkFlow fireBaseWorkFlow;

        public event EventHandler LoggedInStateChanged;

        public bool LoggedIn => fireBaseWorkFlow.LoggedIn;
        public FireBaseViewModel(IFireBaseWorkFlow fireBaseWorkFlow)
        {
            this.fireBaseWorkFlow = fireBaseWorkFlow;
        }

        private void OnLoggedInStateChanged()
        {
            LoggedInStateChanged?.Invoke(this, new EventArgs());
        }
        public void LogIn(string email, string passwort)
        {
            fireBaseWorkFlow.LogIn(email, passwort);
            OnLoggedInStateChanged();
        }

        public void LogOut()
        {
            fireBaseWorkFlow.LogOut();
            OnLoggedInStateChanged();
        }

        public async Task SaveData()
        {
            await fireBaseWorkFlow.SaveDataOnFireBase();
        }

        public async Task LoadData()
        {
            await fireBaseWorkFlow.LoadDataOnFireBase();
        }

        public void Register(string email, string passwort)
        {
            fireBaseWorkFlow.Register(email, passwort);
            OnLoggedInStateChanged();
        }
    }
}
