using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Configuration;
using StagecraftDAL.Interface;


namespace StagecraftDAL.Services
{
    public class LoginService : ILogin
    {
            private readonly string _connectionString;

            public LoginService(IConfiguration configuration)
        {
                _connectionString = configuration.GetConnectionString("DefaultConnection");;
            }

            public async Task<bool> CheckUserExistence(Users user)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("CheckUserExistence", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Password", user.Password);

                        var result = await command.ExecuteScalarAsync();

                        return result != null && (string)result == "true";
                    }
                }
            }
        }

    }
