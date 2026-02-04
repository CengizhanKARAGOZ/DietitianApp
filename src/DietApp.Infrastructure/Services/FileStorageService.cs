using DietApp.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DietApp.Infrastructure.Services;

#pragma warning disable CA1848
public class FileStorageService : IFileStorageService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<FileStorageService> _logger;

    public FileStorageService(IConfiguration configuration, ILogger<FileStorageService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<string> UploadAsync(Stream stream, string fileName, string contentType, string? folder = null, CancellationToken cancellationToken = default)
    {
        // TODO: MinIO veya S3 entegrasyonu yapılacak
        var fileKey = string.IsNullOrEmpty(folder)
            ? $"{Guid.NewGuid()}/{fileName}"
            : $"{folder}/{Guid.NewGuid()}/{fileName}";

        _logger.LogInformation("Dosya yükleniyor: {FileKey}", fileKey);

        await Task.CompletedTask;
        return fileKey;
    }

    public async Task<Stream> DownloadAsync(string fileKey, CancellationToken cancellationToken = default)
    {
        // TODO: MinIO veya S3 entegrasyonu yapılacak
        _logger.LogInformation("Dosya indiriliyor: {FileKey}", fileKey);

        await Task.CompletedTask;
        return Stream.Null;
    }

    public async Task DeleteAsync(string fileKey, CancellationToken cancellationToken = default)
    {
        // TODO: MinIO veya S3 entegrasyonu yapılacak
        _logger.LogInformation("Dosya siliniyor: {FileKey}", fileKey);

        await Task.CompletedTask;
    }

    public async Task<string> GetPresignedUrlAsync(string fileKey, int expirationMinutes = 60, CancellationToken cancellationToken = default)
    {
        // TODO: MinIO veya S3 entegrasyonu yapılacak
        _logger.LogInformation("Presigned URL oluşturuluyor: {FileKey}", fileKey);

        await Task.CompletedTask;
        return $"/files/{fileKey}";
    }
}
#pragma warning restore CA1848
