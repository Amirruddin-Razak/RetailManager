using Caliburn.Micro;
using RMWPFUserInterface.EventModels;
using RMWPFUserInterface.Library.Api.Helpers;
using RMWPFUserInterface.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMWPFUserInterface.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private SalesViewModel _salesVM;
        private IEventAggregator _event;
        ILoggedInUserModel _user;
        IApiHelper _apiHelper;

        public ShellViewModel(IEventAggregator eventAggregator, SalesViewModel salesVM, ILoggedInUserModel user, IApiHelper apiHelper)
        {
            _salesVM = salesVM;
            _event = eventAggregator;
            _user = user;
            _apiHelper = apiHelper;

            _event.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public bool IsLoggedIn
        {
            get
            {
                bool output = true;

                if (string.IsNullOrWhiteSpace(_user.Token))
                {
                    output = false;
                }

                return output;
            }
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public void UserManagement()
        {
            ActivateItem(IoC.Get<UserDisplayViewModel>());
        }

        public void ExitApplication()
        {
            TryClose();
        }

        public void LogOut()
        {
            _user.LogOut();
            _apiHelper.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
