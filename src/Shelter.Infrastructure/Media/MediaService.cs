namespace Shelter.Infrastructure.Media;

public class MediaService : IMediaService
{

    public Task<string> UploadImageAsyc(string path, StreamReader streamReader, CancellationToken cancellationToken)
    {
        // TODO: Implement the method
        return Task.FromResult($"https://picsum.photos/200");
    }
}