using Microsoft.AspNetCore.Http;
using StagecraftDAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL.Services
{
    public class MockFileUploadService : IFileUpload
    {
        public Task<bool> UploadFileAsync(byte[] fileData, string fileName)
        {
            // מימוש התנהגות התחלתי
            return Task.FromResult(true);
        }
    }
}