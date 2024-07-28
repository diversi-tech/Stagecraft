using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL.Interface
{
    public interface IFileUpload
    {
        Task<bool> UploadFileAsync(byte[] fileData, string fileName);
    }
}
