namespace Shelter.API.Features.Animals.Images.GetImages;

public record GetImagesResponse(IEnumerable<GetImageResponse> Images);

public record GetImageResponse(string Url, string MediaType);