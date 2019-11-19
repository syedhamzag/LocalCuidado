using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpRequestExtension
    {
       
        public static async Task<string> UploadFileAsync(this HttpRequest request, IHostingEnvironment env, IFormFile formFile,string name,string path)
        {
            // string filePath = GetFilePath(env,formFile.FileName,name);
            string fileName = string.Concat(name, "_", Path.GetFileNameWithoutExtension(formFile.FileName), Path.GetExtension(formFile.FileName));
            string filePath = Path.Combine(env.ContentRootPath, "Uploads", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return fileName;
        }

       //static string GetFilePath(IHostingEnvironment env, string filename,string name)
       // {
       //     string fileName = string.Concat(name,"_",Path.GetFileNameWithoutExtension(filename), Path.GetExtension(filename));
       //     string filePath = Path.Combine(env.ContentRootPath, "Uploads", fileName);
       //     return filePath;
       // }
    }
}
