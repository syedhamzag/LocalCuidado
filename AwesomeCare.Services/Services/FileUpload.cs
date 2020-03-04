using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeCare.Services.Services
{
    public class FileUpload : IFileUpload
    {
        private IConfiguration _configuration;
        private BlobContainerClient blobContainerClient;
        public FileUpload(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadFile(string folder, bool isPublic, string filename, Stream fileStream)
        {
            string storageConnectionString = _configuration["BlobConnectionString"];
            blobContainerClient = new BlobContainerClient(storageConnectionString, folder);
            await blobContainerClient.CreateIfNotExistsAsync();

            // Set the permissions so the blobs are public. 
            if (isPublic)
                await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.Blob);

            BlobClient blob = blobContainerClient.GetBlobClient(filename);
            fileStream.Position = 0;
            await blob.UploadAsync(fileStream, true);
            return blob.Uri.AbsoluteUri;
        }

        public async Task<Tuple<Stream, string>> DownloadFile(string filename)
        {
            var blobUriBuilder = new Azure.Storage.Blobs.BlobUriBuilder(new Uri(filename));
            var containerName = blobUriBuilder.BlobContainerName;
            var blobname = blobUriBuilder.BlobName;
            string storageConnectionString = _configuration["BlobConnectionString"];
            blobContainerClient = new BlobContainerClient(storageConnectionString, containerName);

            BlobClient blob = blobContainerClient.GetBlobClient(blobname);
            var stream = new MemoryStream();

            var properties = await blob.GetPropertiesAsync();
            var contentType = properties.Value.ContentType;
            await blob.DownloadToAsync(stream);
            return new Tuple<Stream, string>(stream, contentType);


        }
    }
}
