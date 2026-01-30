namespace DietApp.Application.Common.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadAsync(Stream stream, string fileName, string contentType, string? folder = null, CancellationToken cancellationToken = default);
    Task<Stream> DownloadAsync(string fileKey, CancellationToken cancellationToken = default);
    Task DeleteAsync(string fileKey, CancellationToken cancellationToken = default);
    Task<string> GetPresignedUrlAsync(string fileKey, int expirationMinutes = 60, CancellationToken cancellationToken = default);
}
