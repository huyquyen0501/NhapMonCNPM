using Abp.UI;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhapMonCNPM.Helper.MyUploadFile
{
    public static class UploadFile
    {
        private static readonly string[] supportedTypes = new[] { "jpg", "jpeg", "png" };
        public static async Task<string> UploadAsync(string fileLocation, IFormFile file, bool renameFile = true)
        {
            var fileExt = Path.GetExtension(file.FileName).Substring(1).ToLower();
            if (!supportedTypes.Contains(fileExt))
                throw new UserFriendlyException("Wrong file type");

            var fileName = renameFile ? $"{DateTime.Now.ToFileTime()}.{fileExt}" : file.FileName;
            var filePath = Path.Combine(fileLocation, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }

        public static string CreateFolderIfNotExists(string path1, string path2)
        {
            var fileLocation = Path.Combine(path1, path2);
            if (!Directory.Exists(fileLocation))
                Directory.CreateDirectory(fileLocation);

            return fileLocation;
        }
    }
}
