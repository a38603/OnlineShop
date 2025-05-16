using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework
{
    public class BrandModel
    {
        private OnlineShopDbContext context = null;

        public BrandModel()
        {
            context = new OnlineShopDbContext();
        }

        // Lấy danh sách tất cả các thương hiệu
        public List<Brand> ListAll()
        {
            var list = context.Database.SqlQuery<Brand>("Sp_Brand_listAll").ToList();
            return list;
        }


        public int Update(int brandId, string brandName, string logo)
        {
            var parameters = new object[]
            {
                new SqlParameter("@BrandId", brandId),
                new SqlParameter("@BrandName", brandName),
                new SqlParameter("@Logo", logo ?? (object)DBNull.Value)
            };

            return context.Database.ExecuteSqlCommand("EXEC Sp_Brand_Update @BrandId, @BrandName, @Logo", parameters);
        }


        // Xóa thương hiệu theo ID
        public int DeleteBrand(int brandId)
        {
            object[] parameter =
            {
                new SqlParameter("@BrandId", brandId)
            };
            // In ra kiểm tra xem có gọi đúng không
            Console.WriteLine("Xóa brand với ID: " + brandId);
            // Gọi Stored Procedure Sp_Brand_Delete để xóa danh mục
            int res = context.Database.ExecuteSqlCommand("EXEC Sp_Brand_Delete @BrandId", parameter);
            return res;
        }

        // Lấy thông tin thương hiệu theo ID
        public Brand GetById(int brandId)
        {
            return context.Brands.Find(brandId);
        }
        public int Create(string brandName, String logo)
        {
            // Tạo tham số cho stored procedure
            object[] parameters =
            {
                new SqlParameter("@brandName", brandName),
                new SqlParameter("@Logo", string.IsNullOrEmpty(logo) ? (object)DBNull.Value : logo)
            };

            // Thực thi stored procedure Sp_Brand_Insert với tham số đã truyền
            int res = context.Database.ExecuteSqlCommand("EXEC Sp_Brand_Insert @BrandName,@logo", parameters);

            return res; // Trả về số bản ghi bị ảnh hưởng
        }

    }
}
