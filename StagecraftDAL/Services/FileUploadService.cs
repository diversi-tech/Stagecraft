using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using StagecraftDAL.Interface;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;

namespace StagecraftDAL.Services
{
    public class FileUploadService : IFileUpload
    {
        private readonly CloudStorageSettings _cloudStorageSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public FileUploadService(IOptions<CloudStorageSettings> cloudStorageSettings, IHttpClientFactory httpClientFactory)
        {
            _cloudStorageSettings = cloudStorageSettings.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> UploadFileAsync(byte[] fileData, string fileName)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new ByteArrayContent(fileData), "file", fileName);

                var response = await client.PostAsync(_cloudStorageSettings.ApiUrl, content);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
        }
    }
}
