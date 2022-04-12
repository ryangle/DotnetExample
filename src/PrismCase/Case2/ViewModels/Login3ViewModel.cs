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

public class Login3ViewModel : BindableBase, IDialogAware
{
    private IEventAggregator _eventAggregator;

    public event Action<IDialogResult> RequestClose;

    public DelegateCommand LoginCommand { get; private set; }

    public string Title => "登录";

    public Login3ViewModel(IEventAggregator eventAggregator)
    {
        LoginCommand = new DelegateCommand(ExecuteLogin);
        _eventAggregator = eventAggregator;
    }
    private void ExecuteLogin()
    {
        var buttonResult = ButtonResult.OK;
        var parameters = new DialogParameters();
        RequestClose?.Invoke(new DialogResult(buttonResult, parameters));
    }

    public bool CanCloseDialog()
    {
        return true;
    }

    public void OnDialogClosed()
    {
        
    }

    public void OnDialogOpened(IDialogParameters parameters)
    {
    }
}
