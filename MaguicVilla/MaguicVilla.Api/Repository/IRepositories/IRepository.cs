using System.Linq.Expressions;

namespace MaguicVilla.Api.Repository.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task Create(T entity);
        Task Grabar();  
        Task Remover(T entity);
        Task<List<T>> ObtenerTodos(Expression<Func<T,bool>>? filtro=null);
        Task<T> Obtener(Expression<Func<T,bool>> filtro=null,bool tracked=true);
    }
}
