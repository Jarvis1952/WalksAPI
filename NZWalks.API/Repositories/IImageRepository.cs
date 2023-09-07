using NZWalks.API.Models.Domain;
using System.Net;

namespace NZWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Images> Upload(Images image);
    }
}
