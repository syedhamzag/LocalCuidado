using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeCare.Services.Services
{
    public class CompressUploadFile : IFileUpload
    {
       
        public Task<Tuple<Stream, string>> DownloadFile(string filename)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadFile(string folder, bool isPublic, string filename, Stream fileStream)
        {
            throw new NotImplementedException();
        }
    }
}
