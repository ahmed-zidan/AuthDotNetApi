using AuthApi.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Container
{
    public class FileHandlerService : IFileHandlerService
    {
        private readonly IWebHostEnvironment _env;
       
        public FileHandlerService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void createDirectory(string path)
        {
            System.IO.Directory.CreateDirectory(path);
        }
        public void removeDirectory(string path)
        {
            System.IO.Directory.Delete(path);
        }

        public async Task createFileAsync(string path, IFormFile form)
        {
            using(FileStream stream = System.IO.File.Create(path))
            {
                await form.CopyToAsync(stream);
            }
           
        }

        public string getProductImagesPath(int productCode)
        {
            return _env.WebRootPath + "\\Upload\\Product\\" + productCode;
        }

        public bool isDirictoryExist(string path)
        {
            return System.IO.Directory.Exists(path);
        }

        public bool isFileExist(string path)
        {
            return System.IO.File.Exists(path);
        }

        public void removeFile(string path)
        {
            System.IO.File.Delete(path);
        }
    }
}
