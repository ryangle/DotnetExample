using LiteDB;
using LiteDBCase;

// Open database (or create if doesn't exist)
using var db1 = new LiteDatabase(@"Filename=litedbcase.db;Connection=Shared;");
using var db2 = new LiteDatabase(@"Filename=litedbcase.db;Connection=Shared;");
using var db3 = new LiteDatabase(@"Filename=litedbcase.db;Connection=Shared;");
using var db4 = new LiteDatabase(@"Filename=litedbcase.db;Connection=Shared;");
using var db5 = new LiteDatabase(@"Filename=litedbcase.db;Connection=Shared;");
// Get a collection (or create, if doesn't exist)
var col1 = db1.GetCollection<Customer>("customers");
var col2 = db2.GetCollection<Customer>("customers");
var col3 = db3.GetCollection<Customer>("customers");
var col4 = db4.GetCollection<Customer>("customers");


var col5 = db1.GetCollection<Product>("products");
object locker = new object();
Task.Factory.StartNew(() =>
{
    while (true)
    {
        Console.WriteLine($"insert Customer 1");
        // Create your new customer instance
        var customer = new Customer
        {
            Name = "John Doe",
            Phones = new string[] { "8000-0000", "9000-0000" },
            IsActive = true
        };

        // Insert new customer document (Id will be auto-incremented)
        lock (locker)
        {
            col1.Insert(customer);
        }
        Thread.Sleep(100);
    }
});

Task.Factory.StartNew(() =>
{
    while (true)
    {
        Console.WriteLine($"insert Customer 2");
        // Create your new customer instance
        var product = new Product
        {
            Name = "Phone",
            IsActive = true
        };

        // Insert new customer document (Id will be auto-incremented)
        lock (locker)
        {
            col5.Insert(product);
        }
        Thread.Sleep(100);
    }
});

//Task.Factory.StartNew(() =>
//{
//    while (true)
//    {
//        Console.WriteLine($"insert Customer 3");
//        // Create your new customer instance
//        var customer = new Customer
//        {
//            Name = "John Doe",
//            Phones = new string[] { "8000-0000", "9000-0000" },
//            IsActive = true
//        };

//        // Insert new customer document (Id will be auto-incremented)
//        col3.Insert(customer);
//        Thread.Sleep(100);
//    }
//});

//Task.Factory.StartNew(() =>
//{
//    while (true)
//    {
//        // Create your new customer instance
//        Console.WriteLine($"there are {col4.Query().Count()} Customers");
//        Thread.Sleep(100);
//    }
//});

// Update a document inside a collection
//customer.Name = "Jane Doe";

//col.Update(customer);

// Index document using document Name property
//col.EnsureIndex(x => x.Name);

//// Use LINQ to query documents (filter, sort, transform)
//var results = col.Query()
//    .Where(x => x.Name.StartsWith("J"))
//    .OrderBy(x => x.Name)
//    .Select(x => new { x.Name, NameUpper = x.Name.ToUpper() })
//    .Limit(10)
//    .ToList();

//Console.WriteLine(results.Count);

//// Let's create an index in phone numbers (using expression). It's a multikey index
//col.EnsureIndex(x => x.Phones);

//// and now we can query phones
//var r = col.FindOne(x => x.Phones.Contains("8888-5555"));
//if (r != null)
//{
//    Console.WriteLine(r.Name);
//}
//else
//{
//    Console.WriteLine("Not find");
//}
Console.ReadLine();
