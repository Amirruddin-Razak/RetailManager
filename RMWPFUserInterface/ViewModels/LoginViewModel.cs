using Caliburn.Micro;
using RMWPFUserInterface.EventModels;
using RMWPFUserInterface.Library.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMWPFUserInterface.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName = "Amirruddinabdulrazak@gmail.com";
        private string _password = "Pwd.123";
        private string _errorMessage;
        private IApiHelper _apiHelper;
        private IEventAggregator _event;

        public LoginViewModel(IApiHelper apiHelper, IEventAggregator eventAggregator)
        {
            _apiHelper = apiHelper;
            _event = eventAggregator;
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }

        public bool IsErrorVisible => ErrorMessage?.Length > 0;

        public bool CanLogIn
        {
            get
            {
                bool output = false;

                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public async Task LogIn()
        {
            try
            {
                ErrorMessage = "";
                var result = await _apiHelper.Authenticate(UserName, Password);

                await _apiHelper.GetLoggedInUserInfo();

                await _event.PublishOnUIThreadAsync(new LogOnEvent());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }
    }
}
