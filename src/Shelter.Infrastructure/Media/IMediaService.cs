namespace Shelter.Infrastructure.Media;

public interface IMediaService
{
    Task<string> UploadImageAsyc(string path, StreamReader streamReader, CancellationToken cancellationToken);
}