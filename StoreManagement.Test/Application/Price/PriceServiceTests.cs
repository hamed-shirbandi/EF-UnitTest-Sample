using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreManagement.Application.Prices;

namespace StoreManagement.Test.Application.Price
{
    [TestClass]
    public class PriceServiceTests
    {


        [TestMethod]
        public void Get_Higher_Price()
        {
            //Arrange

            var priceService = new PriceService();
            var price1 = 100;
            var price2 = 200;

            //Act
            var higherPrice = priceService.GetHigherPrice(price1,price2);

            //Assert
            Assert.AreEqual(price2, higherPrice);
        }


        [TestMethod]
        public void No_Higher_Price_Less_Than_20_Toman()
        {
            //Arrange

            var priceService = new PriceService();
            var price1 = 10;
            var price2 = 19;

            //Act
            var higherPrice = priceService.GetHigherPrice(price1, price2);

            //Assert
            Assert.IsTrue(higherPrice>=20);
        }
    }
}
