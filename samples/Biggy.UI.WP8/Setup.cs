using Biggy.JSON;
using BiggySamples.Core.Services;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using Microsoft.Phone.Controls;
using System;

namespace Biggy.UI.WP8
{
    public class Setup : MvxPhoneSetup
    {
        public Setup(PhoneApplicationFrame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            var store = new JsonStore<Product>();
            var list = new BiggyList<Product>(store);
            BiggySamples.Core.Services.DataContext.Products = list;
            return new BiggySamples.Core.App();
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}