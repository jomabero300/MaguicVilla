using MaguicVilla.Api.Data;
using MaguicVilla.Api.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MaguicVilla.Api.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        internal DbSet<T> _dbset;

        public Repository(ApplicationDbContext context)
        {
            _context = context;

            _dbset = _context.Set<T>();
        }

        public async Task Create(T entity)
        {
            await _dbset.AddRangeAsync(entity);

            await Grabar();
        }

        public async Task Grabar()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true)
        {
            IQueryable<T> query= _dbset;

            if(!tracked)
            {
                query = query.AsNoTracking();
            }

            if(filtro !=null)
            {
                query = query.Where(filtro);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = _dbset;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            return await query.ToListAsync();
        }

        public async Task Remover(T entity)
        {
            _dbset.Remove(entity);

            await Grabar();
        }
    }
}
