using Case2.ViewModels;
using Case2.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Case2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            //return Container.Resolve<Shell1>();//方案1
            return Container.Resolve<Main2>();//方案2、3

        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AllContent>();
            containerRegistry.RegisterForNavigation<Login1>();
            containerRegistry.RegisterForNavigation<Main1>();
            containerRegistry.RegisterForNavigation<Shell1>();

            containerRegistry.RegisterDialog<Login3>();
        }
        protected override void InitializeShell(Window shell)
        {
            #region 方案2
            //Login2 loginView = Container.Resolve<Login2>();

            //if (loginView.ShowDialog() == true)
            //{
            //    base.InitializeShell(shell);
            //}
            //else
            //{
            //    Application.Current.Shutdown(-1);
            //}
            #endregion

            #region 方案3
            Login3 loginView = Container.Resolve<Login3>();
            var dialogService = Container.Resolve<DialogService>();
            var loginsucess = true;
            dialogService.ShowDialog("Login3", (r) =>
            {
                loginsucess = r.Result == ButtonResult.OK;
            });
            if (loginsucess)
            {
                base.InitializeShell(shell);
            }
            else
            {
                Application.Current.Shutdown(-1);
            }
            #endregion
        }
    }
}
