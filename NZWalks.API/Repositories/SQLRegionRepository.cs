using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();

        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var regionFromDb = await _dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
            if (regionFromDb == null)
                return null;

            regionFromDb.Code = region.Code;
            regionFromDb.Name = region.Name;
            regionFromDb.RegionImageUrl = region.RegionImageUrl;

            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var regionFromDb = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionFromDb == null)
                return null;

            _dbContext.Regions.Remove(regionFromDb);
            await _dbContext.SaveChangesAsync();
            return regionFromDb;
        } 
    }
}
