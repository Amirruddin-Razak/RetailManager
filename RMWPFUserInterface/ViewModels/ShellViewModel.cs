using Caliburn.Micro;
using RMWPFUserInterface.EventModels;
using RMWPFUserInterface.Library.Api.Helpers;
using RMWPFUserInterface.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RMWPFUserInterface.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _event;
        ILoggedInUserModel _user;
        IApiHelper _apiHelper;

        public ShellViewModel(IEventAggregator eventAggregator, ILoggedInUserModel user, IApiHelper apiHelper)
        {
            _event = eventAggregator;
            _user = user;
            _apiHelper = apiHelper;

            _event.SubscribeOnPublishedThread(this);

            ActivateItemAsync(IoC.Get<LoginViewModel>());
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

        public async Task UserManagement()
        {
            await ActivateItemAsync(IoC.Get<UserDisplayViewModel>());
        }

        public void ExitApplication()
        {
            TryCloseAsync();
        }

        public async Task LogOut()
        {
            _user.LogOut();
            _apiHelper.LogOffUser();
            await ActivateItemAsync(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(IoC.Get<SalesViewModel>(), cancellationToken);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
