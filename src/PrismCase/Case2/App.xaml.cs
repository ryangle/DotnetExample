﻿using Case2.Views;
using Prism.DryIoc;
using Prism.Ioc;
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
            return Container.Resolve<Shell1View>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
