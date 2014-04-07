# Sample using biggy with xamarin

- Mvvmcross
- Xamarin
- Biggy

Its not PCL yet, but it works....

# Biggy: A Very Fast Document/Relational Query Tool with Full LINQ Compliance

I like working with Document databases, and I like working with relational ones. I like LINQ, I like Postgres, and sometimes I just want to store data on disk in a JSON file: **so I made Biggy**.

This project started life as an implementation of ICollection<T> that persisted itself to a file using JSON seriliazation. That quickly evolved into using Postgres as a JSON store, and then SQL Server. What we ended up with is the fastest data tool you can use.

Data is loaded into memory when your application starts, and you query it with Linq. That's it. It loads incredibly fast (100,000 records in about 1 second) and from there will sync your in-memory list with whatever store you choose. 


## File-based Document Storage
If you don't want to install a database engine, you don't have to. Biggy can load and write to disk easily:

```csharp
class Product {
  public String Sku { get; set; }
  public String Name { get; set; }
  public Decimal Price { get; set; }
  public DateTime CreatedAt { get; set; }

  public Product() {
    this.CreatedAt = DateTime.Now;
  }

  public override bool Equals(object obj) {
    var p1 = (Product)obj;
    return this.Sku == p1.Sku;
  }
} 

//add and save to this list as above
//this will create a Data/products.json file in your project/site root
var products = new BiggyList<Product>(new JsonStore<Product>());

var newProduct = new Product{Sku : "STUFF", Name : "A new product", Price : 120.00};

//gets appended immediately in a single line-write to file
products.Add(newProduct);

//this won't hit the disk as you're querying in-memory only
var p = products.FirstOrDefault(x => x.Sku == "STUFF");
p.Name = "Something Fun";

//this writes to disk in a single asynchronous flush - so it's fast too
products.Update(p);
```

You can move from the file store over to the relational store by a single type change (as well as moving data over). This makes Biggy attractive for greenfield projects and just trying stuff out.


More [https://github.com/robconery/biggy](https://github.com/robconery/biggy)