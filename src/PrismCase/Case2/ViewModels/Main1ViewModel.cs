using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2.ViewModels
{
    public class Main1ViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        public DelegateCommand ExitCommand { get; private set; }
        public Main1ViewModel(IEventAggregator eventAggregator)
        {
            ExitCommand = new DelegateCommand(Exit);
            _eventAggregator = eventAggregator;
        }
        private void Exit()
        {
            _eventAggregator.GetEvent<LoginEvent>().Publish(false);
        }
    }
}
