using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
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

        public ProductManager(IProductDal productDal)
        {
            this.productDal = productDal;
        }

        // Validate by rules in Product Validator
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //business codes

            // logging
            //cacheremove
            //performance
            //transaction
            //authorazation

            this.productDal.Add(product);
            return new SuccessResult( Messages.ProductAdded );
        }

        public IDataResult<List<Product>> GetAll()
        {

            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

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
    }
}
