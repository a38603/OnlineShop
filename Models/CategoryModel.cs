using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework
{
    public class CategoryModel
    {
        private OnlineShopDbContext context = null;

        public CategoryModel()
        {
            context = new OnlineShopDbContext();
        }

        // Lấy danh sách tất cả danh mục
        public List<Category> ListAll()
        {
            var list = context.Database.SqlQuery<Category>("Sp_Category_listAll").ToList();
            return list;
        }

        // Thêm mới một danh mục
        public int Create(string categoryName)
        {
            object[] parameter =
            {
                new SqlParameter("@CategoryName", categoryName)
            };
            int res = context.Database.ExecuteSqlCommand("EXEC Sp_Category_Insert @CategoryName", parameter);
            return res;
        }

        // Cập nhật thông tin danh mục
        public int Update(int categoryId, string categoryName)
        {
            object[] parameter =
            {
                new SqlParameter("@CategoryId", categoryId),   // Chỉnh sửa tên tham số cho đúng
                new SqlParameter("@CategoryName", categoryName) // Đảm bảo tên tham số giống trong stored procedure
            };

            // Gọi Stored Procedure Sp_Category_Update để cập nhật thông tin danh mục
            int res = context.Database.ExecuteSqlCommand("EXEC Sp_Category_Update @CategoryId, @CategoryName", parameter);
            return res;
        }

        // Lấy danh mục theo ID
        public Category GetById(int categoryId)
        {
            var category = context.Categories.Find(categoryId);
            return category;
        }

        // Xóa một danh mục theo ID
        public int DeleteCategory(int categoryId)
        {
            object[] parameter =
            {
                new SqlParameter("@CategoryId", categoryId)
            };

            // Gọi Stored Procedure Sp_Category_Delete để xóa danh mục
            int res = context.Database.ExecuteSqlCommand("EXEC Sp_Category_Delete @CategoryId", parameter);
            return res;
        }
    }
}
