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

        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM products");
        }
    }
}
