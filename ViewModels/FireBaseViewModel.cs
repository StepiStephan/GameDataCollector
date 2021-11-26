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

        public bool LoggedIn => fireBaseWorkFlow.LoggedIn;
        public FireBaseViewModel(IFireBaseWorkFlow fireBaseWorkFlow)
        {
            this.fireBaseWorkFlow = fireBaseWorkFlow;
        }

        public void LogIn(string email, string passwort)
        {
            fireBaseWorkFlow.LogIn(email, passwort);
        }

        public void LogOut()
        {
            fireBaseWorkFlow.LogOut();
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
        }
    }
}
