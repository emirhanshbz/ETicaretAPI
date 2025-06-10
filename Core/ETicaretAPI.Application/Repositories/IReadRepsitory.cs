using ETicaretAPI.Domain.Entities.Common;
using System.Linq.Expressions;

namespace ETicaretAPI.Application.Repositories
{
    //read işlemleri yani get(SELECT) işlemleri için kullanılacak
    public interface IReadRepsitory<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T > GetAll(bool tracking = true); //Tüm verileri getir
        IQueryable<T> GetWhere(Expression<Func<T , bool>> method, bool tracking = true); //Belirli bir koşula göre verileri getir
        Task<T> GetSingleAsync(Expression<Func<T , bool>> method, bool tracking = true); //Belirli bir koşula göre tek bir veri getir
        Task<T> GetByIdAsync(string id, bool tracking = true); //Id'ye göre veri getir
    }
}
