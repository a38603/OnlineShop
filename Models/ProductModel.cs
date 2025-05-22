using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Models.Framework;
using Models.ViewModels;
using System.Web.Mvc;

namespace Models
{
    public class ProductModel
    {

        private OnlineShopDbContext context = new OnlineShopDbContext();
        public ProductModel()
        {
            context = new OnlineShopDbContext();
        }

        // Lấy danh sách tất cả các thương hiệu
        public List<Product> ListAll()
        {
            var list = context.Database.SqlQuery<Product>("EXEC Sp_Product_listAll").ToList();
            return list;
        }


        public ProductEditViewModel GetById(int productId)
        {
            return context.Database.SqlQuery<ProductEditViewModel>("EXEC Sp_Product_GetById @ProductId",new SqlParameter("@ProductId", productId)).FirstOrDefault();
                
            
        }
        public int Create(string productName, decimal price, string productImg, int categoryId, int brandId, int stock, decimal? rating,DateTime createdAt)
            {
                object[] parameters =
                {
                    new SqlParameter("@ProductName", productName),
                    new SqlParameter("@Price", price),
                    new SqlParameter("@ProductImg", string.IsNullOrEmpty(productImg) ? (object)DBNull.Value : productImg),
                    new SqlParameter("@CategoryId", categoryId),
                    new SqlParameter("@BrandId", brandId),
                    new SqlParameter("@Stock", stock),
                    new SqlParameter("@Rating", rating ?? (object)DBNull.Value),
                    new SqlParameter("@CreatedAt", createdAt == DateTime.MinValue ? (object)DBNull.Value : createdAt)
                };

                int res = context.Database.ExecuteSqlCommand("EXEC Sp_Product_Insert @ProductName, @Price, @ProductImg, @CategoryId, @BrandId, @Stock, @Rating, @CreatedAt", parameters);
                return res;
            }
        // Cập nhật sản phẩm
        public int Update(int productId, string productName, decimal price, string productImg, int categoryId, int brandId, int stock, decimal rating, DateTime createdAt)
        {
            object[] parameters =
            {
                new SqlParameter("@ProductId", productId),
                new SqlParameter("@ProductName", productName),
                new SqlParameter("@Price", price),
                new SqlParameter("@ProductImg", string.IsNullOrEmpty(productImg) ? (object)DBNull.Value : productImg),
                new SqlParameter("@CategoryId", categoryId),
                new SqlParameter("@BrandId", brandId),
                new SqlParameter("@Stock", stock),
                new SqlParameter("@Rating", rating),
                new SqlParameter("@CreatedAt", createdAt)
            };

            int res = context.Database.ExecuteSqlCommand(
                "EXEC Sp_Product_Update @ProductId, @ProductName, @Price, @ProductImg, @CategoryId, @BrandId, @Stock, @Rating, @CreatedAt", parameters);

            return res;
        }

        public int DeleteProduct(int productId)
        {
            object[] parameter =
            {
                new SqlParameter("@ProductId", productId)
            };
            // In ra kiểm tra xem có gọi đúng không
            Console.WriteLine("Xóa brand với ID: " + productId);
            // Gọi Stored Procedure Sp_Brand_Delete để xóa danh mục
            int res = context.Database.ExecuteSqlCommand("EXEC Sp_Product_Delete @ProductId", parameter);
            return res;
        }
        public List<ProductDisplayModel> GetAllProducts()
        {
            return context.Database.SqlQuery<ProductDisplayModel>("EXEC Sp_Product_listAll").ToList();
        }
        public List<ProductViewModel> GetProductsByCategory(int categoryId)
        {
            return context.Database.SqlQuery<ProductViewModel>(
                "EXEC Sp_Product_GetByCategory @CategoryId",
                new SqlParameter("@CategoryId", categoryId)).ToList();
        }
        public List<ProductViewModel> GetProductsByBrand(int brandId)
        {
            return context.Database.SqlQuery<ProductViewModel>(
                "EXEC Sp_Product_GetByBrand @BrandId",
                new SqlParameter("@BrandId", brandId)
            ).ToList();
        }

    }
}

