using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using Biggy.Core.ViewModels;
using Biggy.JSON;

namespace Biggy.Droid.Views
{
	[Activity(Label = "View for ProductViewModel")]
	public class ProductView : MvxActivity
    {
		public new ProductViewModel ViewModel 
		{
			get { return base.ViewModel as ProductViewModel; }
			set { base.ViewModel = value; }
		}

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			SetContentView(Resource.Layout.ProductView);

			ViewModel.List = new BiggyList<Product> (new JsonStore<Product> ());
        }
    }
}