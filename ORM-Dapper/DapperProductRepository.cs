using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    internal class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            _conn.Execute("INSERT INTO products (Name, Price, CategoryID) VALUES (@name, @price, @categoryID)", new { name, price, categoryID});
        }

        public void UpdateProduct(Product product)
        {
            _conn.Execute("UPDATE products SET Name = @name, Price = @price, CategoryID = @catID, OnSale = @onSale, StockLevel = @stock WHERE ProductID = @id;",
                new {id = product.ProductID, name = product.Name, price = product.Price, catID = product.CategoryID, onSale = product.OnSale, stock = product.StockLevel});
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM products");
        }

        public Product GetProduct(int id)
        {
            return _conn.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = @id;", new {id});
        }

        public void DeleteProduct(int id)
        {
            _conn.Execute("DELETE FROM reviews WHERE ProductID = @id", new {id});
            _conn.Execute("DELETE FROM sales WHERE ProductID = @id", new { id });
            _conn.Execute("DELETE FROM products WHERE ProductID = @id", new { id });
        }
    }
}
