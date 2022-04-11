using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Case2.ViewModels;

public class Login1ViewModel : BindableBase
{
    private IEventAggregator _eventAggregator;
    public DelegateCommand LoginCommand { get; private set; }
    public Login1ViewModel(IEventAggregator eventAggregator)
    {
        LoginCommand = new DelegateCommand(ExecuteLogin);
        _eventAggregator = eventAggregator;
    }
    private void ExecuteLogin()
    {
        _eventAggregator.GetEvent<LoginEvent>().Publish(true);
    }
}
