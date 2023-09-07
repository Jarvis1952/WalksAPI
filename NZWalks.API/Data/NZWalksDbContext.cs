using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions) 
        { 
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; } 
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Images> Images { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed Data for Difficulties 
            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("AC690F97-6A2D-401E-B8BF-29BE66455E11"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("8D69B141-BA8B-443D-9168-CDEB365ABB50"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("E63932C7-3A93-4CCE-B6F5-6C5F79BC11DF"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // seed Data for Regions
            var regions = new List<Region>()
            {
                    new Region
                    {
                        Id = Guid.Parse("77F7F756-F8E9-4C0E-9432-390BEE46215A"),
                        Name = "AuckLand",
                        Code = "AKL",
                        RegionImageUrl = "AuckLand.jpg"
                    },
                    new Region
                    {
                        Id = Guid.Parse("ED40E45B-CA5E-4452-B9F4-0EA688C65326"),
                        Name = "Weelington",
                        Code = "WGN",
                        RegionImageUrl = "Weelington.jpg"
                    },
                    new Region
                    {
                        Id = Guid.Parse("0F5E69F2-3AE8-41E9-A9CF-36CCAD797389"),
                        Name = "Nelson",
                        Code = "NSN",
                        RegionImageUrl = "Nelson.jpg"
                    },
                    new Region
                    {
                        Id = Guid.Parse("1CE9A54A-C3F0-4C8D-9FF6-C509CB0C3113"),
                        Name = "SouthLand",
                        Code = "STL",
                        RegionImageUrl = "SouthLand.jpg"
                    },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
