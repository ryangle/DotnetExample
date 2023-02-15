using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using System.Diagnostics;
using WpfApp1.ViewModels;

namespace WpfApp1;

public class AppBootstrapper : BootstrapperBase
{
    private readonly SimpleContainer _container = new();
    public AppBootstrapper()
    {
        Initialize();
    }

    protected override void Configure()
    {
        _container.Singleton<IWindowManager, WindowManager>();
        _container.Singleton<IEventAggregator, EventAggregator>();
        _container.Singleton<LoginViewModel>();
        _container.Singleton<Login1ViewModel>();
        _container.Singleton<Login2ViewModel>();
        _container.Singleton<MainWindowViewModel>();
    }

    protected override object GetInstance(Type service, string key)
    {
        return _container.GetInstance(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
        return _container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
        _container.BuildUp(instance);
    }
    protected override void OnStartup(object sender, StartupEventArgs e)
    {
        //_ = DisplayRootViewForAsync<LoginViewModel>();
        //_ = DisplayRootViewForAsync<Login1ViewModel>();
        _ = DisplayRootViewForAsync<Login2ViewModel>();
        //_ = DisplayRootViewForAsync<MainWindowViewModel>();
    }
}