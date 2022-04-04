using Caliburn.Micro;
using RMWPFUserInterface.Library.Api;
using RMWPFUserInterface.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMWPFUserInterface.ViewModels
{
    public class UserDisplayViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly StatusInfoViewModel _statusInfoVM;
        private readonly IUserEndpoint _userEndpoint;

        private BindingList<UserModel> _users;
        private BindingList<string> _currentRoles;
        private UserModel _selectedUser;
        private BindingList<string> _availableRoles = new BindingList<string>();
        private string _selectedCurrentRole;
        private string _selectedAvailableRole;

        public UserDisplayViewModel(IWindowManager windowManager, StatusInfoViewModel statusInfoVM, IUserEndpoint userEndpoint)
        {
            _windowManager = windowManager;
            _statusInfoVM = statusInfoVM;
            _userEndpoint = userEndpoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadUser();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";

                if (ex.Message == "Unauthorized")
                {
                    _statusInfoVM.UpdateMessage("Not Authorized", "You do not have permission to access sale fom");
                    await _windowManager.ShowDialogAsync(_statusInfoVM, null, settings);
                }
                else
                {
                    _statusInfoVM.UpdateMessage("Fatal Error", ex.Message);
                    await _windowManager.ShowDialogAsync(_statusInfoVM, null, settings);
                }

                TryCloseAsync();
            }
        }

        private async Task LoadUser()
        {
            var userList = await _userEndpoint.GetAll();
            Users = new BindingList<UserModel>(userList);
        }

        public BindingList<UserModel> Users
        {
            get => _users;
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }

        public BindingList<string> AvailableRoles
        {
            get => _availableRoles;
            set
            {
                _availableRoles = value;
                NotifyOfPropertyChange(() => AvailableRoles);
            }
        }

        public BindingList<string> CurrentRoles
        {
            get => _currentRoles;
            set
            {
                _currentRoles = value;
                NotifyOfPropertyChange(() => CurrentRoles);
            }
        }

        public UserModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                CurrentRoles = new BindingList<string>(value.Roles.Select(x => x.Value).ToList());

                //Todo find better solution
                LoadRoles();
                NotifyOfPropertyChange(() => SelectedUser);
                NotifyOfPropertyChange(() => AvailableRoles);
            }
        }

        public string SelectedCurrentRole
        {
            get => _selectedCurrentRole;
            set
            {
                _selectedCurrentRole = value;
                NotifyOfPropertyChange(() => SelectedCurrentRole);
            }
        }

        public string SelectedAvailableRole
        {
            get => _selectedAvailableRole;
            set
            {
                _selectedAvailableRole = value;
                NotifyOfPropertyChange(() => SelectedAvailableRole);
            }
        }

        private async Task LoadRoles()
        {
            var roles = await _userEndpoint.GetAllRoles();
            foreach (var role in roles)
            {
                if (SelectedUser.Roles.ContainsValue(role.Value) == false)
                {
                    AvailableRoles.Add(role.Value);
                }
            }
        }

        public async Task AddToRole()
        {
            try
            {
                await _userEndpoint.AddUserToRole(SelectedUser.Id, SelectedAvailableRole);
                CurrentRoles.Add(SelectedAvailableRole);
                AvailableRoles.Remove(SelectedAvailableRole);

                CurrentRoles.ResetBindings();
                AvailableRoles.ResetBindings();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task RemoveFromRole()
        {
            try
            {
                await _userEndpoint.RemoveUserFromRole(SelectedUser.Id, SelectedCurrentRole);
                AvailableRoles.Add(SelectedCurrentRole);
                CurrentRoles.Remove(SelectedCurrentRole);

                CurrentRoles.ResetBindings();
                AvailableRoles.ResetBindings();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
