using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeCare.Services.Services
{
   public interface IFileUpload
    {
        Task<string> UploadFile(string folder, bool isPublic, string filename, Stream fileStream);
        Task<Tuple<Stream,string>> DownloadFile(string filename);
    }
}
