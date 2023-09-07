using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly NZWalksDbContext _NZWalksDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalksDbContext nZWalksDbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _HttpContextAccessor = httpContextAccessor;
            _NZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Images> Upload(Images image)
        {
            var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{ image.FileName} { image.FileExtension}");

            using var stream = new FileStream(localPath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{_HttpContextAccessor.HttpContext.Request.Scheme}://" +
                $"{_HttpContextAccessor.HttpContext.Request.Host}" +
                $"{_HttpContextAccessor.HttpContext.Request.PathBase}" +
                $"/Images/" +
                $"{image.FileName}" +
                $"{image.FileExtension}";

            image.FilePath = urlFilePath;

            await _NZWalksDbContext.Images.AddAsync(image);
            await _NZWalksDbContext.SaveChangesAsync();

            return image;
        }
    }
}
