using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Music.Api.Models;
using System.IO;
using System.Threading.Tasks;

namespace Music.Api.Helpers
{
    public static class FileHelper
    {
        public static readonly string connectionString = "";

        public static async Task<string> UploadFile(IFormFile file, string containerName)
        {
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = containerClient.GetBlobClient(file.FileName);
            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream);
            return blobClient.Uri.AbsoluteUri;
        }

        public static string GetFileUrl(IFormFile file, string containerName)
        {
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = containerClient.GetBlobClient(file.FileName);
            return blobClient.Uri.AbsoluteUri;
        }
    }
}
