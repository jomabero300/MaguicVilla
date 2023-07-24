using MaguicVilla.Api.Data;
using MaguicVilla.Api.Models;
using MaguicVilla.Api.Repository.IRepositories;

namespace MaguicVilla.Api.Repository
{
    public class GpsRepository : Repository<GpsTrasabilidad>, IGpsRepository
    {
        private readonly ApplicationDbContext _context;

        public GpsRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }

    }
}
