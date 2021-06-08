using SportsCashier.DataBase;
using SportsCashier.Helpers;
using SportsCashier.Models;
using SportsCashier.Services.DialogService;
using SportsCashier.Services.MessagingService;
using SportsCashier.Services.NavigationService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SportsCashier
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Protected Members

        /// <summary>
        /// A global lock for property checks so prevent locking on different instances of expressions.
        /// Considering how fast this check will always be it isn't an issue to globally lock all callers.
        /// </summary>
        protected object mPropertyValueCheckLock = new object();
        //protected readonly IGenericDbRepository<MemberModel> _membersRepository = DependencyService.Get<IGenericDbRepository<MemberModel>>();
        //protected readonly IGenericDbRepository<PlayerModel> _playersRepository = DependencyService.Get<IGenericDbRepository<PlayerModel>>();
        //protected readonly IGenericDbRepository<Sport> _sportsRepository = DependencyService.Get<IGenericDbRepository<Sport>>();
        //protected readonly IGenericDbRepository<PlayerSport> _ps = DependencyService.Get<IGenericDbRepository<PlayerSport>>();
        //protected readonly IGenericDbRepository<Post> _postRepository = DependencyService.Get<IGenericDbRepository<Post>>();
        //protected readonly IAuthenticationService _authenticationService = DependencyService.Get<IAuthenticationService>();
        //protected readonly IDialogService _dialogService = DependencyService.Get<IDialogService>();
        //protected readonly INavigationService _navigationService = DependencyService.Get<INavigationService>();
        //protected readonly ISettingsService _settingsService = DependencyService.Get<ISettingsService>();


        protected INavigationService _navigationService { get; }
        protected IDialogService _dialogService { get; }
        protected IMessagingService _messagingService => DependencyService.Get<IMessagingService>();
        protected IGenericDbRepository<MemberModel> _membersRepository { get; }
        protected IGenericDbRepository<PlayerModel> _playersRepository { get; }
        protected IGenericDbRepository<Sport> _sportsRepository  { get; }
        protected IGenericDbRepository<PlayerSport> _ps  { get; }
        protected bool IsBusy { get; set; }
        protected bool CommandRun { get; set; }

        #endregion

        #region Public Properteis
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        #endregion

        public BaseViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>();
            _dialogService = DependencyService.Get<IDialogService>();
            _membersRepository = DependencyService.Get<IGenericDbRepository <MemberModel>>();
            _playersRepository = DependencyService.Get<IGenericDbRepository<PlayerModel>>();
            _sportsRepository = DependencyService.Get<IGenericDbRepository<Sport>>();
            _ps = DependencyService.Get<IGenericDbRepository<PlayerSport>>();
            DependencyService.Get<IGenericDbRepository<Invoice>>();
        }


        public virtual Task InitializeAsync() => Task.CompletedTask;

        public virtual Task UninitializeAsync() => Task.CompletedTask;

        #region Command Helpers

        /// <summary>
        /// Runs a command if the updating flag is not set.
        /// If the flag is true (indicating the function is already running) then the action is not run.
        /// If the flag is false (indicating no running function) then the action is run.
        /// Once the action is finished if it was run, then the flag is reset to false
        /// </summary>
        /// <param name="updatingFlag">The boolean property flag defining if the command is already running</param>
        /// <param name="action">The action to run if the command is not already running</param>
        /// <returns></returns>
        protected async Task RunCommandAsync(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            // Lock to ensure single access to check
            lock (mPropertyValueCheckLock)
            {
                // Check if the flag property is true (meaning the function is already running)
                if (updatingFlag.GetPropertyValue())
                    return;

                // Set the property flag to true to indicate we are running
                updatingFlag.SetPropertyValue(true);
            }

            try
            {
                // Run the passed in action
                await action();
            }
            finally
            {
                // Set the property flag back to false now it's finished
                updatingFlag.SetPropertyValue(false);
            }
        }

        /// <summary>
        /// Runs a command if the updating flag is not set.
        /// If the flag is true (indicating the function is already running) then the action is not run.
        /// If the flag is false (indicating no running function) then the action is run.
        /// Once the action is finished if it was run, then the flag is reset to false
        /// </summary>
        /// <param name="updatingFlag">The boolean property flag defining if the command is already running</param>
        /// <param name="action">The action to run if the command is not already running</param>
        /// <typeparam name="T">The type the action returns</typeparam>
        /// <returns></returns>
        protected async Task<T> RunCommandAsync<T>(Expression<Func<bool>> updatingFlag, Func<Task<T>> action, T defaultValue = default(T))
        {
            // Lock to ensure single access to check
            lock (mPropertyValueCheckLock)
            {
                // Check if the flag property is true (meaning the function is already running)
                if (updatingFlag.GetPropertyValue())
                    return defaultValue;

                // Set the property flag to true to indicate we are running
                updatingFlag.SetPropertyValue(true);
            }

            try
            {
                // Run the passed in action
                return await action();
            }
            finally
            {
                // Set the property flag back to false now it's finished
                updatingFlag.SetPropertyValue(false);
            }
        }

        #endregion
    }
}
