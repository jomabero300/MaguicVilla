using MaguicVilla.Api.Models;

namespace MaguicVilla.Api.Repository.IRepositories
{
    public interface IVillaRepository:IRepository<Villa>
    {
        Task<Villa> Actualizar(Villa entidad);
    }
}
