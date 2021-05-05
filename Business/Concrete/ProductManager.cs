 using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        //Bir entity manager kendinden başka bir entity managerı enjekte edemez.****
        
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
            
        }

        //Encryption,Hashing, MD5, SHA1, Salting
        //Claim
        [SecuredOperation("admin,editor")]
        [ValidationAspect(typeof (ProductValidator))]
        //validation
        public IResult Add(Product product)
        {
            //Business Codes
            //magic strings
            
            IResult result=BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckProductsNameEqual(product.ProductName),CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
            
           
        }

        public IDataResult<List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour==22)
            //{
            //    return new ErrorDateResult<List<Product>>(Messages.MaintenanceTime);
            //}
            //İş kodları
            //yetkisi var mı?
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId==productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {

            //Select count(*) from products where categoryId=1
            var result = _productDal.GetAll(p => p.CategoryId ==categoryId).Count;
            if (result > 10)
            {
                return new ErrorResult(Messages.MoreCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckProductsNameEqual(string productname)
        {
            var result = _productDal.GetAll(p => p.ProductName == productname).Count();
            if (result>1)
            {
                return new ErrorResult(Messages.SameProductName);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
       
    }
}
