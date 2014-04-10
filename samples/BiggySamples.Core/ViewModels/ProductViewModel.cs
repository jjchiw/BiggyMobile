using Cirrious.MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Diagnostics;
using Biggy.Core.Services;
using System.Linq;
using Biggy;

namespace BiggySamples.Core.ViewModels
{
	public class ProductViewModel  : MvxViewModel
    {
		private string _sku;
		public string Sku
		{ 
			get { return _sku; }
			set { _sku = value; RaisePropertyChanged(() => Sku); }
		}

		private string _name;
		public string Name
		{ 
			get { return _name; }
			set { _name = value; RaisePropertyChanged(() => Name); }
		}

		private decimal _price;
		public decimal Price
		{ 
			get { return _price; }
			set { _price = value; RaisePropertyChanged(() => Price); }
		}

		private DateTime _createdAt;
		public DateTime CreatedAt
		{ 
			get { return _createdAt; }
			set { _createdAt = value; RaisePropertyChanged(() => CreatedAt); }
		}

		public override bool Equals(object obj) {
			var p1 = (ProductViewModel)obj;
			return this.Sku == p1.Sku;
		}

		public override int GetHashCode (){
			return Sku.GetHashCode ();
		}
			
		public IBiggy<Product> List
		{ 
			get { return DataContext.Products; }
		}

		MvxCommand _addCommand;
		public ICommand AddCommand
		{
			get 
			{
				_addCommand = _addCommand ?? new MvxCommand (DoAddCommand);
				return _addCommand;
			}
		}

		Product ToProduct()
		{
			return new Product {
				Sku = Sku,
				Name = Name,
				Price = Price,
				CreatedAt = DateTime.Now,
			};
		}

		void DoAddCommand()
		{
			Debug.WriteLine ("Doing command");
			DataContext.Products.Add (this.ToProduct ());
			Sku = "";
			Name = "";
			Price = 0.0m;
			CreatedAt = DateTime.MinValue;
		}
    }
}
