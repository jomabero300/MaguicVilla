using MaguicVilla.Api.Data;
using MaguicVilla.Api.Models;
using MaguicVilla.Api.Repository.IRepositories;
using System.Linq.Expressions;

namespace MaguicVilla.Api.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {

        private readonly ApplicationDbContext _context;

        public VillaRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<Villa> Actualizar(Villa entidad)
        {
            entidad.FechaActualizacion=DateTime.Now;

            _context.Villas.Update(entidad);

            await _context.SaveChangesAsync();

            return entidad;
        }
    }
}
