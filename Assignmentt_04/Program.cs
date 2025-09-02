using Assignmentt_04.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

#region Main Program
class Program
{
    static void Main()
    {
        using var db = new NorthwindContext();

        #region LINQ & JOIN
        Console.WriteLine(" LINQ & JOIN ");

        // INNER JOIN
        var innerJoin = from o in db.Orders
                        join c in db.Customers
                        on o.CustomerId equals c.CustomerId
                        select new { o.OrderId, c.CompanyName };

        foreach (var item in innerJoin.Take(5))
            Console.WriteLine($"Order: {item.OrderId}, Customer: {item.CompanyName}");

        // LEFT JOIN
        var leftJoin = from c in db.Customers
                       join o in db.Orders
                       on c.CustomerId equals o.CustomerId into gj
                       from subOrder in gj.DefaultIfEmpty()
                       select new { c.CompanyName, OrderId = subOrder != null ? subOrder.OrderId : 0 };

        foreach (var item in leftJoin.Take(5))
            Console.WriteLine($"Customer: {item.CompanyName}, Order: {item.OrderId}");

        // GROUP JOIN
        var groupJoin = from c in db.Customers
                        join o in db.Orders
                        on c.CustomerId equals o.CustomerId into ordersGroup
                        select new { c.CompanyName, OrdersCount = ordersGroup.Count() };

        foreach (var item in groupJoin.Take(5))
            Console.WriteLine($"Customer: {item.CompanyName}, Orders: {item.OrdersCount}");

        // CROSS JOIN
        var crossJoin = from c in db.Customers
                        from e in db.Employees
                        select new { c.CompanyName, e.FirstName };

        foreach (var item in crossJoin.Take(5))
            Console.WriteLine($"Customer: {item.CompanyName}, Employee: {item.FirstName}");
        #endregion

        #region Loading Related Data
        Console.WriteLine("\n Loading Related Data");

        // EAGER LOADING
        var ordersWithDetails = db.Orders
                                  .Include(o => o.Customer)
                                  .Include(o => o.Employee)
                                  .Take(5)
                                  .ToList();
        foreach (var order in ordersWithDetails)
            Console.WriteLine($"Order {order.OrderId}, Customer: {order.Customer?.CompanyName}, Employee: {order.Employee?.FirstName}");

        // EXPLICIT LOADING
        var firstOrder = db.Orders.First();
        db.Entry(firstOrder).Reference(o => o.Customer).Load();
        db.Entry(firstOrder).Collection(o => o.OrderDetails).Load();
        Console.WriteLine($"Explicit Loaded Order {firstOrder.OrderId} for Customer {firstOrder.Customer?.CompanyName}");

        
        #endregion

        #region Inheritance Mapping
        Console.WriteLine("\nInheritance Mapping ");

        using var appDb = new AppDbContext();
        appDb.Database.EnsureCreated();

        appDb.People.Add(new CustomerPerson { Name = "Ali", Address = "Cairo" });
        appDb.People.Add(new EmployeePerson { Name = "Omar", Salary = 5000 });
        appDb.SaveChanges();

        var people = appDb.People.ToList();
        foreach (var p in people)
            Console.WriteLine($"Person: {p.Name}, Type: {p.GetType().Name}");
        #endregion
    }
}
#endregion
