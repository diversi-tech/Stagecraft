using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL.Services
{
    public class UsersService
    {
        public static bool CheckIfEmailExists(string email)
        {
            SqlParameter param1 = new SqlParameter("@email", email);
            var t = DataAccess.ExecuteStoredProcedure<bool>("CheckIfEmailExists", param1);
            return t;
        }
    }
}
