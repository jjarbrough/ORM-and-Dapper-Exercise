using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            //testing the departments get and add

            var departmentRepo = new DapperDepartmentRepository(conn);
            departmentRepo.InsertDepartment("Office Supplies");
            var departments = departmentRepo.GetAllDepartments();
            foreach (var department in departments)
            {
                Console.WriteLine($"{department.DepartmentID} | {department.Name}");
            }

            //testing the products methods

            var productRepo = new DapperProductRepository(conn);
            productRepo.CreateProduct("Pen", 1.99, 5);
            var products = productRepo.GetAllProducts();
            foreach(var product in products)
            {
                Console.WriteLine($"{product.Name} | {product.Price} | {product.CategoryID}");
            }

        }
    }
}
