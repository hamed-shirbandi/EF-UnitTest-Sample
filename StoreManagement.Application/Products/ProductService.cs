using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagement.Core.Helpers;
using StoreManagement.Application.Products.Dto;
using StoreManagement.InfraStructure.Context;
using StoreManagement.Core.Domain;
using System.Data.Entity;

namespace StoreManagement.Application.Products
{
    public class ProductService : IProductService
    {
        #region Properties

        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Product> _products;



        #endregion


        #region Ctor


        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
            _products = _uow.Set<Product>();
        }


        #endregion


        #region Public Methods



        /// <summary>
        /// 
        /// </summary>
        public PublicJsonResult Create(ProductInput input)
        {
            if (_products.Any(p=>p.Title==input.Title))
            {
                return new PublicJsonResult { result=false};
            }
            var product = new Product
            {
                Id = input.Id,
                Title = input.Title,
                Price = input.Price,
                NewPrice = input.NewPrice,
            };
            _products.Add(product);
    
            return new PublicJsonResult { result = true };
        }




        /// <summary>
        /// 
        /// </summary>
        public PublicJsonResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return new PublicJsonResult { result = false };
            }

            _uow.MarkAsDeleted(product);
            return new PublicJsonResult { result = true };

        }





        /// <summary>
        /// 
        /// </summary>
        public ProductInput Get(int id)
        {
            var product = _products.FirstOrDefault(p=>p.Id==id);
            if (product==null)
            {
                return new ProductInput { Id=0};
            }

            return new ProductInput
            {
                Id=product.Id,
                Title=product.Title
            };
        }





        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ProductInput> Search(string term)
        {
            return _products.Select(p => new ProductInput
            {
                Id = p.Id,
                Title = p.Title
            }).ToList();
        }




        /// <summary>
        /// 
        /// </summary>
        public PublicJsonResult Trash(int id)
        {
            throw new NotImplementedException();
        }




        /// <summary>
        /// 
        /// </summary>
        public PublicJsonResult Update(ProductInput input)
        {
            throw new NotImplementedException();
        }


        #endregion


        #region Private Methods





        #endregion


    }
}
