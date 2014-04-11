using System;
using BiggySamples.Core.ViewModels;
using Biggy;

namespace BiggySamples.Core.Services
{
	public class DataContext
	{
		public static IBiggy<Product> Products { get; set; }
	}

	public class Product
	{
		public string Sku { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}

