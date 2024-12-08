using Microsoft.EntityFrameworkCore;
using QueryAndChangeDataSamples.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAndChangeDataSamples
{
    public class QuerySamples
    {
        [Test]
        public void GetAll()
        {
            using (var db = new NorthwindContext())
            {
                var categories = db.Categories.ToList();
                foreach (var category in categories)
                {
                    Console.WriteLine(category.CategoryName);
                }
            }
        }


        [Test]
        [TestCase(1)]
        [TestCase(15)]
        public void GetById(int id)
        {
            using (var db = new NorthwindContext())
            {
                var category = db.Categories.Find(id);

                var message = category == null
                    ? "not found"
                    : category.CategoryName;
                Console.WriteLine($"Category with id={id}: {message}");
            }
        }

        [Test]
        public void LinqQuery()
        {
            using (var db = new NorthwindContext())
            {
                var products = from p in db.Products
                               where p.Discontinued && p.UnitsInStock > 10
                               select p;

                foreach (var p in products)
                {
                    Console.WriteLine($"{p.ProductName} - {p.UnitsInStock}");
                }
            }
        }

        [Test]
        public void SqlQuery()
        {
            using (var db = new NorthwindContext())
            {
                var products = db.Products
                    .FromSql($"""
                        select * from Northwind.Products 
                        where Discontinued = 1 and UnitsInStock > 10
                    """)
                    .ToList();

                foreach (var p in products)
                {
                    Console.WriteLine($"{p.ProductName} - {p.UnitsInStock}");
                }
            }
        }
    }
}