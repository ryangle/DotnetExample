using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string? _windowTitle;
        public string? WindowTitle
        {
            get { return _windowTitle; }
            set { SetProperty(ref _windowTitle, value); }
        }
        public MainWindowViewModel()
        {
            WindowTitle = "Case2";
        }
    }
}
