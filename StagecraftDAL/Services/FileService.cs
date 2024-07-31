using StagecraftDAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL.Services
{
    public class FileService : IFile
    {
        public const string FileAdminPath = @"C:\\Users\\משתמש\\Desktop\\dowenload_files\\adminFiles\\";
        public string DownloadTaskFiles(int VideoId)
        {
            SqlParameter param1 = new SqlParameter("@VideoId", VideoId);
            var t = SQLDataAccess.ExecuteStoredProcedure<string>("DownloadTaskFiles", param1);

            return Path.Combine(FileAdminPath, t);
        }

        //public const string FileUserPath = @"C:\\Users\\משתמש\\Desktop\\dowenload_files\\userFiles\\";
        //public string DownloadTaskFiles(int VideoId)
        //{
        //    SqlParameter param1 = new SqlParameter("@VideoId", VideoId);
        //    var t = SQLDataAccess.ExecuteStoredProcedure<string>("DownloadTaskFiles", param1);

        //    return Path.Combine(FileUserPath, t);
        //}
    }
}
