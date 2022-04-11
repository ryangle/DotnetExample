using Case2.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace Case2.ViewModels;

public class Shell1ViewModel : BindableBase
{
    private IRegionManager _regionManager;
    private IContainerProvider _containerProvider;
    private IEventAggregator _eventAggregator;
    private IRegion _shellRegion;
    private Login1 _loginView;
    private Main1 _mainView;
    public DelegateCommand LoadedCommand { get; private set; }
    public Shell1ViewModel(IRegionManager regionManager, IContainerProvider containerProvider, IEventAggregator eventAggregator)
    {
        LoadedCommand = new DelegateCommand(ExecuteLoaded);
        _regionManager = regionManager;
        _containerProvider = containerProvider;
        _eventAggregator = eventAggregator;
        _eventAggregator.GetEvent<LoginEvent>().Subscribe(LoginMessageHandler);

        _loginView = _containerProvider.Resolve<Login1>();
        _mainView = _containerProvider.Resolve<Main1>();
    }
    private void ExecuteLoaded()
    {
        _shellRegion = _regionManager.Regions["ShellRegion"];
        _shellRegion.Add(_loginView);
        _shellRegion.Add(_mainView);
    }
    private void LoginMessageHandler(bool loginState)
    {
        if (loginState)
        {
            //_mainContentRegion.Deactivate(_loginView);
            _shellRegion.Activate(_mainView);
        }
        else
        {
            //_mainContentRegion.Deactivate(_mainView);
            _shellRegion.Activate(_loginView);
        }
    }
}
