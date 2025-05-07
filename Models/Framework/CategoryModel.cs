using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework
{
    public class CategoryModel
    {
        private OnlineShopDbContext context=null;
        public CategoryModel()
        {
            context = new OnlineShopDbContext();
        }
        public List<Category> ListAll()
        {
            var list= context.Database.SqlQuery<Category>("Sp_Category_ListAll").ToList();
            return list;
        }
    }
}
