using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreManagement.InfraStructure.Context;
using Moq;
using StoreManagement.Application.Products;
using StoreManagement.Application.Products.Dto;
using System.Collections.Generic;
using System.Linq;
using StoreManagement.Core.Domain;
using System.Data.Entity;

namespace StoreManagement.Test.Application.Products
{
    [TestClass]
    public class ProductServiceTests
    {
        public static ProductService _productService;


        [ClassInitialize]
        public static void ProductServiceTestspre(TestContext context)
        {

            var products = GetFakeProduct();
            IQueryable<Product> data = products.AsQueryable();
            var mockProductSet = new Mock<DbSet<Product>>();
            mockProductSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockProductSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockProductSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockProductSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockProductSet.Setup(d => d.Add(It.IsAny<Product>())).Callback<Product>((s) => products.Add(s));

            var mainContext = new Mock<IUnitOfWork>();
            mainContext.Setup(m => m.Set<Product>()).Returns(mockProductSet.Object);
            mainContext.Setup(d => d.MarkAsDeleted(It.IsAny<Product>())).Callback<Product>((s) => products.Remove(s));

            _productService = new ProductService(mainContext.Object);
        }

    
        [TestMethod]
        public void Can_Create_product()
        {
            //Arrange
            var product = new ProductInput
            {
                Id = 99,
                Title = "p1",
                Price = 30,
                NewPrice = 40
            };

            //Act
            var response = _productService.Create(product);
            var savedProduct = _productService.Get(product.Id);


            //Assert
            Assert.IsTrue(response.result);
            Assert.AreEqual(savedProduct.Id, product.Id);
        }




        [TestMethod]
        public void Product_Title_Shoulde_Be_Unique()
        {
            //Arrange
            var product = new ProductInput
            {
                Id = 100,
                Title = "A",
                Price = 30,
                NewPrice = 40
            };

            //Act
            var response = _productService.Create(product);
    

            //Assert
            Assert.IsFalse(response.result);
          
        }




        [TestMethod]
        public void Can_Delete_Product()
        {
            //Arrange
            var productId = 1;

            //Act
            var response = _productService.Delete(productId);
            var savedProduct = _productService.Get(productId);


            //Assert
            Assert.IsTrue(response.result);
            Assert.AreEqual(savedProduct.Id, 0);
        }




        private static List<Product> GetFakeProduct()
        {
            return new List<Product>
         {
             new Product
             {
                 Id = 1,
                 Title = "A"
             },
             new Product
             {
                 Id = 2,
                 Title = "B"
             },
             new Product
             {
                 Id = 3,
                 Title = "C"
             }
         };
        }



    }
}
