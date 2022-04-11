using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2.ViewModels
{
    public class Main2ViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private IRegionManager _regionMannager;
        public DelegateCommand LoadedCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public Main2ViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            LoadedCommand = new DelegateCommand(ExecuteLoaded);
            ExitCommand = new DelegateCommand(Exit);
            _eventAggregator = eventAggregator;
            _regionMannager = regionManager;

        }
        private void ExecuteLoaded()
        {
            //_regionMannager.RegisterViewWithRegion("MainRegion", "AllContent");
            _regionMannager.RequestNavigate("MainRegion", "AllContent");
        }
        private void Exit()
        {
            _eventAggregator.GetEvent<LoginEvent>().Publish(false);
        }
    }
}
