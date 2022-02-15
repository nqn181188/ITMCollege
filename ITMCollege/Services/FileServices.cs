using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollege.Services
{
    public class FileServices
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FileServices(IWebHostEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }
        private string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        [Obsolete]
        public FileContentResult GetFile(string filename)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", filename);

            var mimeType = this.GetMimeType(filename);

            byte[] fileBytes;
                fileBytes = File.ReadAllBytes(filepath);
            return new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = filename
            };
        }

    }
}
