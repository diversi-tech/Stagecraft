using System;
using System.IO;
using System.Threading.Tasks;
using Common;
using iText.Forms.Form.Element;
using Microsoft.AspNetCore.Http;


namespace StagecraftBL
{
    public static class FilesBL
    {
        private static string _filePath = @"C:\AAA";

        public static async Task<string> SaveFileAsync(IFormFile file, FileSaveStatus status, string filePath = null)
        {
            try
            {
                string path = null;

                if (status == FileSaveStatus.Delete)
                {
                    // Handle file deletion
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        path = Path.Combine(_filePath, filePath);
                        if (System.IO.File.Exists(path))

                        {
                            System.IO.File.Delete(path);
                            return $"File deleted: {path}";
                        }
                        else
                        {
                            throw new FileNotFoundException("The file to delete was not found.", path);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("No file path provided for deletion.");
                    }
                }

                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("No file uploaded.");
                }

                path = Path.Combine(_filePath, file.FileName);

                // Ensure the directory exists
                var directoryPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Save the file (for Add and Update operations)
                using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(stream);
                }

                return path;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new ApplicationException("An error occurred while processing the file.", ex);
            }
        }
    }
}
