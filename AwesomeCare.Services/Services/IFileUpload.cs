using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeCare.Services.Services
{
   public interface IFileUpload
    {
        Task<string> UploadFile(string folder, bool isPublic, string filename, Stream fileStream, string contentType);
        Task<Tuple<Stream,string>> DownloadFile(string filename);
        MemoryStream DownloadClientFile(Object entity);
    }
}
