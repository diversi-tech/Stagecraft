using System.Data.SqlClient;
using Common;
using StagecraftDAL.Interface;

namespace StagecraftDAL.Services
{
    public class SignupService : ISignup
    {
        public int RegisterUser(Users user)
        {
            SqlParameter param1 = new SqlParameter("@UserName", user.Name);
            SqlParameter param2 = new SqlParameter("@PasswordHash", user.Password);
            SqlParameter param3 = new SqlParameter("@Email", user.Email);
            SqlParameter param4 = new SqlParameter("@Status", user.Status.HasValue ? user.Status.Value : false); // ברירת מחדל ל-false אם לא מוגדר

            try
            {
                var userId = SQLDataAccess.ExecuteStoredProcedure<int>("RegisterUser", param1, param2, param3, param4);
                return userId;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000 && ex.Message.Contains("Email already exists."))
                {
                    throw new InvalidOperationException("Email already exists.");
                }
                throw;
            }
        }
    }
}
