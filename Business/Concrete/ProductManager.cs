using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal productDal;
        private ICategoryService categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            this.productDal = productDal;
            this.categoryService = categoryService;
        }

        //Claim
        //[SecuredOperation("product.add,admin")]
        
        // Validate by rules in Product Validator
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {

            // Cross Cutting Concerns
            // logging
            //cacheremove
            //performance
            //transaction
            //authorazation   

            

            // traverse business rules and check them one by one
            IResult result = BusinessRules.Run(
                CheckIfProductCountOfCategoryCorrect(product.CategoryId), 
                CheckIfProductNameExists(product.ProductName),
                CheckIfCategoryLimitExceeded()

                );

            if(result != null)
            {
                return result;
            }
            this.productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
    
        }

        public IDataResult<List<Product>> GetAll()
        {

            //if (DateTime.Now.Hour == 1)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}

            //Add Business Rules
            return new SuccessDataResult<List<Product>>(productDal.GetAll(), Messages.ProductsListed );
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(productDal.GetAll(p => p.CategoryId == id), "Products are got by category id.");
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(this.productDal.Get(p => p.ProductId == productId), "Product is got by id.");
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max), "Products are got by unit price.");
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(productDal.GetProductDetails(),"Product details are got.");
        }
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = productDal.GetAll(p => p.CategoryId == categoryId ).Count;
            if (result >= 10)
            {
                return new ErrorResult("A category can has  maximum 10 product.");
            }

            return new SuccessResult();

        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult("Product name already exists.");
            }

            return new SuccessResult();

        }

        private IResult CheckIfCategoryLimitExceeded()
        {
            int numbOfCategory = categoryService.GetAll().Data.Count;

            if (numbOfCategory > 15)
            {
                return new ErrorResult("Number of category can not be greater than 15.");
            }

            return new SuccessResult(); 
        }

    }
}
