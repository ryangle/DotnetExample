using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;

namespace WpfApp1.ViewModels
{
    public class Login2ViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        public ObservableCollection<string> UserList { get; set; } = new ObservableCollection<string>();

        public string SelectedUser { get; set; } = null!;
        /// <summary>
        /// 交易密码
        /// </summary>
        public string Password { get; set; } = null!;
        /// <summary>
        /// 终端认证码
        /// </summary>
        public string AuthCode { get; set; } = null!;
        /// <summary>
        /// 登录按钮是否禁用
        /// </summary>
        public bool CanLogin { get; set; } = true;
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; } = null!;

        public string Version { get; set; } = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "";

        public string CopyRight { get; set; } = $"Copyright © 2016-{DateTime.UtcNow.AddHours(8).Year} All Rights Reserved.";

        public bool LoginEnabled { get; set; } = true;

        public Login2ViewModel(
            IWindowManager windowManager,
            IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            DisplayName = string.Empty;
        }

        protected override void OnViewLoaded(object view)
        {
            _eventAggregator.SubscribeOnBackgroundThread(this);
            base.OnViewLoaded(view);
        }
        protected override void OnViewReady(object view)
        {
#if SIMSERVER
            _windowManager.ShowWindowAsync(_debugView).Wait();
#endif
            base.OnViewReady(view);
        }
        public override Task TryCloseAsync(bool? dialogResult = null)
        {
            _eventAggregator.Unsubscribe(this);
            return base.TryCloseAsync(dialogResult);
        }

        public void OnUserChanged(ComboBox comboBox)
        {
            
        }

        public void OnPasswordChanged(PasswordBox source)
        {
            Password = source.Password;
        }

        public void OnAuthCodeChanged(PasswordBox source)
        {
            AuthCode = source.Password;
        }

        public async Task Login()
        {
            
        }

        public async Task Register()
        {
            await Task.CompletedTask;
        }

        public void DeleteUserInfo(Button button)
        {
            
        }

    }
}