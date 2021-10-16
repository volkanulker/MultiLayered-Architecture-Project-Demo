using LinqProject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Category> categories = new List<Category>
            {
                new Category{CategoryId=1, CategoryName="Bilgisayar"},
                new Category{CategoryId=2, CategoryName="Telefon"},

            };

            List<Product> products = new List<Product>
            {
                new Product{ProductId=1, CategoryId=1, ProductName="Acer Laptop", QuantityPerUnit="32 GB RAM", UnitPrice=10000, UnitsInStock=5},
                new Product{ProductId=2, CategoryId=1, ProductName="Asus Laptop", QuantityPerUnit="16 GB RAM", UnitPrice=8000, UnitsInStock=3},
                new Product{ProductId=3, CategoryId=1, ProductName="Hp Laptop", QuantityPerUnit="8 GB RAM", UnitPrice=6000, UnitsInStock=2},
                new Product{ProductId=4, CategoryId=2,ProductName="Samsung Telefon", QuantityPerUnit="4 GB RAM", UnitPrice=5000, UnitsInStock=15},
                new Product{ProductId=5, CategoryId=2, ProductName="Apple Telefon", QuantityPerUnit="4 GB RAM", UnitPrice=8000, UnitsInStock=0},


            };

            //Console.WriteLine("Standard Algorithm");

            //foreach (var product in products)
            //{

            //    if (product.UnitPrice > 5000 && product.UnitsInStock > 3)
            //    {
            //        Console.WriteLine(product.ProductName);

            //    }
            //}

            //Console.WriteLine("Lingq--------------------------------------");

            //List<Product> result = products.Where(p => p.UnitPrice > 5000 && p.UnitsInStock > 3).ToList();


            // Any
            //Console.WriteLine(AnyTest(products));

            // Find
            //FindTest(products);

            // Find All
            //FindAllTest(products);

            //WithFromUsage(products);


            ClassicLinqTest(products, categories);

        }

        private static void WithFromUsage(List<Product> products)
        {
            var result = from p in products
                         where p.UnitPrice > 6000
                         orderby p.UnitPrice
                         select p;

            foreach (Product p in result)
            {
                Console.WriteLine(p.ProductName + "|| unit price =>"+ p.UnitPrice);
            }
        }

        private static void FindAllTest(List<Product> products)
        {
            List<Product> foundProducts = products.Where(p => p.ProductName.Contains("top")).OrderBy(p => p.UnitPrice).ToList();
            foreach (Product tempProduct in foundProducts)
            {
                Console.WriteLine(tempProduct.ProductName);
            }

        }

        private static void FindTest(List<Product> products)
        {
            Product foundProduct = products.Find(p => p.ProductId == 3);
            Console.WriteLine(foundProduct.ProductName);
        }

        private static bool AnyTest(List<Product> products)
        {
            return products.Any(p => p.ProductName == "Acer Laptop");
        }

        static List<Product> GetProducts(List<Product> products)
        {
            return products.Where(p => p.UnitPrice > 5000 && p.UnitsInStock > 3).ToList();
        }

        static void ClassicLinqTest(List<Product> products, List<Category> categories)
        {
            var result = from p in products
            join c in categories on p.CategoryId equals c.CategoryId
            select new ProductDto { ProductId = p.ProductId, ProductName = p.ProductName, UnitPrice = p.UnitPrice };

            foreach (ProductDto productDto in result)
            {
                Console.WriteLine(productDto.ProductName);
            }

            
        }




    }

    class ProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
