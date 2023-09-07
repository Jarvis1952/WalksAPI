using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid Id)
        {
            var walkDomain = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);

            if (walkDomain == null)
                return null;

            _dbContext.Walks.Remove(walkDomain);
            await _dbContext.SaveChangesAsync();
            return walkDomain;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid Id)
        {
            return await _dbContext.Walks.Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Walk?> UpdateAsync(Guid Id, Walk walk)
        {
            var walkDomain = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);

            if (walkDomain == null)
                return null;

            walkDomain.Name = walk.Name;
            walkDomain.Description = walk.Description;
            walkDomain.RegionId = walk.RegionId;
            walkDomain.LengthInKm = walk.LengthInKm;
            walkDomain.WalkImageUrl = walk.WalkImageUrl;    
            walkDomain.DifficultyId = walk.DifficultyId;

            await _dbContext.SaveChangesAsync();

            return walkDomain;
        }
    }
}
