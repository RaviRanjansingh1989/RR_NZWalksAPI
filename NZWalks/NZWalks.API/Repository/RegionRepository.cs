using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;
        public RegionRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id=Guid.NewGuid();
            await nZWalksDBContext.AddAsync(region);
            await nZWalksDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
           var region= await nZWalksDBContext.Region.FirstOrDefaultAsync(s => s.Id == id);
            if (region == null)
            {
                return null;
            }
            nZWalksDBContext.Region.Remove(region);
            await nZWalksDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalksDBContext.Region.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await nZWalksDBContext.Region.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id,Region region)
        {
            var existingregions = await nZWalksDBContext.Region.FirstOrDefaultAsync(s => s.Id == id);
            if (existingregions == null)
            {
                return null;
            }
            existingregions.Code = region.Code;
            existingregions.Name = region.Name;
            existingregions.Population= region.Population;
            existingregions.Lat= region.Lat;
            existingregions.Area= region.Area;
            existingregions.Long= region.Long;

            await nZWalksDBContext.SaveChangesAsync();
            return existingregions;
        }
    }
}
