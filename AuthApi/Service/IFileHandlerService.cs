using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Service
{
    public interface IFileHandlerService
    {
        string getProductImagesPath(int productCode);
        bool isDirictoryExist(string path);
        bool isFileExist(string path);
        void createDirectory(string path);
        void removeDirectory(string path);
        void removeFile(string path);
        Task createFileAsync(string path, IFormFile form);
        
    }
}
