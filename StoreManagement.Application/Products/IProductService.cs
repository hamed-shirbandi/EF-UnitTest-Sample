using StoreManagement.Core.Helpers;
using StoreManagement.Application.Products.Dto;
using System.Collections.Generic;

namespace StoreManagement.Application.Products
{
    public interface IProductService
    {
        ProductInput Get(int id);
        IEnumerable<ProductInput> Search(string term);
        PublicJsonResult Create(ProductInput input);
        PublicJsonResult Update(ProductInput input);
        PublicJsonResult Trash(int id);
        PublicJsonResult Delete(int id);
    }
}
