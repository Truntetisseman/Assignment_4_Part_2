using EfEx.Domain;
using System.Collections.Generic;

namespace DataServiceLib
{
    public interface IDataService
    {
        IList<Category> GetCategories();
        Category GetCategory(int id);
        IList<Product> GetProducts();
        Product GetProduct(int id);
        bool CreateCategory(Category tocreate);
        bool UpdateCategory(Category c);
        bool DeleteCategory(int id);
    }
}