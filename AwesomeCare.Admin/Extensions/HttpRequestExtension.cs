using Dropbox.Api;
using Dropbox.Api.Files;
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
        [Obsolete]
        public static async Task<string> UploadFileAsync(this HttpRequest request, IHostingEnvironment env, IFormFile formFile, string name, string path)
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

        public static async Task<string> UploadFileToDropboxAsync(this HttpRequest request, DropboxClient dropboxClient, IFormFile formFile, string folderName, string fileName)
        {
            string path = $"/{folderName}/{fileName}";
            await dropboxClient.Files.UploadAsync(path, body: formFile.OpenReadStream());
            var sharedsettings = new Dropbox.Api.Sharing.SharedLinkSettings(Dropbox.Api.Sharing.RequestedVisibility.Public.Instance, audience: Dropbox.Api.Sharing.LinkAudience.Public.Instance);
            var sharedLink = await dropboxClient.Sharing.CreateSharedLinkWithSettingsAsync(path, sharedsettings);
            string link = sharedLink.Url.Replace("?dl=0", "?raw=1");
            return link;
        }

        public static async Task UpdateDropboxFileAsync(this HttpRequest request, DropboxClient dropboxClient, IFormFile formFile, string folderName, string fileName)
        {
            string path = $"/{folderName}/{fileName}";
            await dropboxClient.Files.UploadAsync(path, WriteMode.Overwrite.Instance, body: formFile.OpenReadStream());
        }


        //static string GetFilePath(IHostingEnvironment env, string filename,string name)
        // {
        //     string fileName = string.Concat(name,"_",Path.GetFileNameWithoutExtension(filename), Path.GetExtension(filename));
        //     string filePath = Path.Combine(env.ContentRootPath, "Uploads", fileName);
        //     return filePath;
        // }
    }
}
