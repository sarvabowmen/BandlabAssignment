using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Banlab.Social.Api.Services.Interfaces;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Banlab.Social.Api.Services
{
    public class ImageService(BlobContainerClient containerClient) : IImageService
    {
        private readonly BlobContainerClient _containerClient = containerClient;

        private readonly StorageTransferOptions _storageTransferOptions = new()
        {
            InitialTransferSize = 1 * 1024 * 1024, //1MB
            MaximumTransferSize = 104_857_600,
            MaximumConcurrency = 5,
        };

        public async Task<string> UploadImage(Stream stream, bool IsOriginalImage, string filename)
        {
            var folder = IsOriginalImage ? "original" : "processed";
            var blobClient = _containerClient.GetBlobClient($"{folder}/{filename}");
            await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                TransferOptions = _storageTransferOptions
            });
            return blobClient.Uri.ToString();
        }

        public async Task<Stream> ResizeImage(Stream originalImage, int width, int height, bool resetStream)
        {
            using var imageStream = Image.Load(originalImage);
            imageStream.Mutate(x => x.Resize(width, height));
            var outputStream = new MemoryStream();
            await imageStream.SaveAsJpegAsync(outputStream);
            if (resetStream)
                outputStream.Seek(0, SeekOrigin.Begin);
            return outputStream;
        }
    }
}
