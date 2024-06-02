using AuthApi.Helper;
using AuthApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthApi.Controllers
{
    
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileHandlerService _fileHandler;
      
        public ProductController(IUnitOfWork uow, IFileHandlerService fileHandler)
        {
            _uow = uow;
            _fileHandler = fileHandler;
        }

        [HttpPut("uploadImg/{productId}")]
        public async Task<IActionResult> uploadImg(IFormFile form , int productId)
        {
            ApiResponse api = new ApiResponse();
            if (!ModelState.IsValid)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "Data is not valid";
                return BadRequest(api);
            }

            string path = _fileHandler.getProductImagesPath(productId);
            if (!_fileHandler.isDirictoryExist(path))
            {
                _fileHandler.createDirectory(path);
            }
            string imgPath = path + "\\" + productId + ".png";
            if (_fileHandler.isFileExist(imgPath))
            {
                System.IO.File.Delete(imgPath);
            }

            await _fileHandler.createFileAsync(imgPath, form);
            api.ResponseCode = 201;
            api.Result = imgPath;
            return Ok(api);
            
        }

        [HttpPut("uploadMultImg/{productId}")]
        public async Task<IActionResult> uploadMultImg(IFormFileCollection form, int productId)
        {
            ApiResponse api = new ApiResponse();
            if (!ModelState.IsValid)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "Data is not valid";
                return BadRequest(api);
            }

            string path = _fileHandler.getProductImagesPath(productId);
            if (!_fileHandler.isDirictoryExist(path))
            {
                _fileHandler.createDirectory(path);
            }
            int passCnt = 0, errCnt = 0;
            for(int i = 0; i < form.Count; i++)
            {
                try
                {
                    string imgPath = path + "\\" + form[i].FileName;
                    if (_fileHandler.isFileExist(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                    await _fileHandler.createFileAsync(imgPath, form[i]);
                    passCnt++;
                }
                catch
                {
                    errCnt++;
                }
            }
           
            api.ResponseCode = 201;
            api.Result = passCnt+" Passed, " + errCnt+ " Failed";
            return Ok(api);

        }

        [HttpGet("GetImage/{productId}")]
        public IActionResult GetImage(int productId)
        {
            ApiResponse api = new ApiResponse();
            if (!ModelState.IsValid)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "Data is not valid";
                return BadRequest(api);
            }

            string productPath = _fileHandler.getProductImagesPath(productId);
            string imgPath = productPath + "\\" + productId + ".png";
            string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            if (_fileHandler.isFileExist(imgPath))
            {
                api.ResponseCode = 200;
                api.Result = hostUrl + "\\Upload\\Product\\" + productId+ "\\" + productId + ".png";
                return Ok(api);
            }
            else
            {
                api.ResponseCode = 404;
                api.ErrorMessage = "Not Found";
                return NotFound(api);
            }

            

        }

        [HttpGet("GetImages/{productId}")]
        public IActionResult GetImages(int productId)
        {
            ApiResponse api = new ApiResponse();
            if (!ModelState.IsValid)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "Data is not valid";
                return BadRequest(api);
            }

            string productPath = _fileHandler.getProductImagesPath(productId);
            string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            List<string> imgUrls = new List<string>();
            
            if (_fileHandler.isDirictoryExist(productPath))
            {

                DirectoryInfo dir = new DirectoryInfo(productPath);
                FileInfo[] files = dir.GetFiles();
                foreach(var file in files)
                {
                    imgUrls.Add(hostUrl + "\\Upload\\Product\\" + productId + "\\" + file.Name);
                }

                api.ResponseCode = 200;
                api.Result = string.Join(',', imgUrls);
                return Ok(api);
            }
            else
            {
                api.ResponseCode = 404;
                api.ErrorMessage = "Not Found";
                return NotFound(api);
            }
        }


        [HttpGet("Download/{productId}")]
        public async Task<IActionResult> DownloadAsync(int productId)
        {
            ApiResponse api = new ApiResponse();
            if (!ModelState.IsValid)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "Data is not valid";
                return BadRequest(api);
            }

            string productPath = _fileHandler.getProductImagesPath(productId);
            string imgPath = productPath + "\\" + productId + ".png";
            if (_fileHandler.isFileExist(imgPath))
            {
                MemoryStream memoryStream = new MemoryStream();
                using (FileStream stream = new FileStream(imgPath , FileMode.Open))
                {
                    await stream.CopyToAsync(memoryStream);
                }
                memoryStream.Position = 0;

                return File(memoryStream , "image/png",productId+".png");
                
            }
            else
            {
                api.ResponseCode = 404;
                api.ErrorMessage = "Not Found";
                return NotFound(api);
            }



        }

        [HttpGet("remove/{productId}")]
        public IActionResult remove(int productId)
        {
            ApiResponse api = new ApiResponse();
            if (!ModelState.IsValid)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "Data is not valid";
                return BadRequest(api);
            }

            string productPath = _fileHandler.getProductImagesPath(productId);
            string imgPath = productPath + "\\" + productId + ".png";
            if (_fileHandler.isFileExist(imgPath))
            {
                _fileHandler.removeFile(imgPath);
                return Ok();
            }
            else
            {
                api.ResponseCode = 404;
                api.ErrorMessage = "Not Found";
                return NotFound(api);
            }



        }

        [HttpGet("removeFiles/{productId}")]
        public IActionResult removeFiles(int productId)
        {
            ApiResponse api = new ApiResponse();
            if (!ModelState.IsValid)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "Data is not valid";
                return BadRequest(api);
            }

            string productPath = _fileHandler.getProductImagesPath(productId);
            if (_fileHandler.isDirictoryExist(productPath))
            {
                DirectoryInfo dir = new DirectoryInfo(productPath);
                FileInfo[] files = dir.GetFiles();
                foreach (var file in files)
                {
                    file.Delete();
                }
                return Ok();
            }
            else
            {
                api.ResponseCode = 404;
                api.ErrorMessage = "Not Found";
                return NotFound(api);
            }
        }


    }
}
