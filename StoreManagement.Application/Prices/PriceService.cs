using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Prices
{
   public class PriceService : IPriceService
    {
        #region Properties


        #endregion


        #region Ctor


        public PriceService()
        {
    
        }


        #endregion


        #region Public Methods



        /// <summary>
        /// 
        /// </summary>
        public int GetHigherPrice(int price1,int price2)
        {
           
            var higherPrice= price1 > price2 ? price1 : price2;
            return higherPrice >= 20 ? higherPrice : 20;
        }




        #endregion


        #region Private Methods





        #endregion


    }
}
