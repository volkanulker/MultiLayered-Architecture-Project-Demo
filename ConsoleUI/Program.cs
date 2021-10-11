using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            InMemoryProductDal inMemoryProductDal = new InMemoryProductDal();
            ProductManager productManager = new ProductManager(inMemoryProductDal);
            foreach(var product in productManager.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }
        
        }
    }
}
