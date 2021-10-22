using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProductTest();

            //CategoryTest();

            ProductDetailDtoTest();

        }

        private static void CategoryTest()
        {
            ICategoryDal efCategoryDal = new EfCategoryDal();
            CategoryManager categoryManager = new CategoryManager(efCategoryDal);

            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);

            }
        }

        private static void ProductTest()
        {
            //EfProductDal efProductDal = new EfProductDal();
            //ProductManager productManager = new ProductManager(efProductDal);

            //foreach (var product in productManager.GetAllByCategoryId(2))
            //{
            //    Console.WriteLine(product.ProductName);
            //}
        }

        private static void ProductDetailDtoTest()
        {
            EfProductDal efProductDal = new EfProductDal();
            ProductManager productManager = new ProductManager(efProductDal);

            foreach (var product in productManager.GetProductDetails().Data)
            {
                Console.WriteLine("[" +product.ProductId+ "] " + "["+ product.UnitsInStock+"] "
                    + "["+product.ProductName+"]"+ "[" + product.CategoryName + "]");
            }
        }
    }
}
