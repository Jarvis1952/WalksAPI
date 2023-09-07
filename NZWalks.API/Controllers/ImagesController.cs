using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;

        public ImagesController(IMapper mapper,IImageRepository imageRepository) 
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO imageUploadRequest)
        {
            ValidateFileuplaod(imageUploadRequest);

            if(ModelState.IsValid)
            {
                // convert dto to domain model

                var imageDomainModel = new Images
                {
                    File = imageUploadRequest.File,
                    FileExtension = Path.GetExtension(imageUploadRequest.File.FileName),
                    FileSizeInBytes = imageUploadRequest.File.Length,
                    FileName = imageUploadRequest.FileName,
                    Description = imageUploadRequest.Description
                };

                await _imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
                
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileuplaod(ImageUploadRequestDTO imageUploadRequest)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtension.Contains(Path.GetExtension(imageUploadRequest.File.FileName))) 
            {
                ModelState.AddModelError("file", "Unsupported file extension");   
            }

            if(imageUploadRequest.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10 MB, Please Upload a smaller size file.");
            }
        }
    }
}
