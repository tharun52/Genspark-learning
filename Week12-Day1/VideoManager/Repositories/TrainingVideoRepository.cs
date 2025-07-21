using Microsoft.EntityFrameworkCore;
using VideoManager.Data;
using VideoManager.Models;

namespace VideoManager.Repositories
{
    public class TrainingVideoRepository : Repository<Guid, TrainingVideo>
    {
        public TrainingVideoRepository(AppDbContext appDbContext) : base(appDbContext)
        { }
        public override async Task<TrainingVideo?> Get(Guid key)
        {
            return await _appDbContext.TrainingVideos
                   .SingleOrDefaultAsync(p => p.Id == key);
        }

        public override async Task<IEnumerable<TrainingVideo>> GetAll()
        {
            return await _appDbContext.TrainingVideos.ToListAsync();
        }
    }
}