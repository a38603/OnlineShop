using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;

namespace Models
{
    public class AccountModel
    {
        private OnlineShopDbContext context = null;
        public AccountModel()
        {
            context = new OnlineShopDbContext();
        }
        public bool Login(string username, string password)
        {
            object[] sqlParams =
            {
                new SqlParameter("@UserName", username),
                new SqlParameter("@PassWord", password)
            };
            var res = context.Database.SqlQuery<bool>("EXEC Sp_Account_login @UserName, @PassWord",sqlParams).SingleOrDefault();
            return res;
        }
    }
}
