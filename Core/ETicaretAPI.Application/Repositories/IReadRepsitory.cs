using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    //read işlemleri yani get(SELECT) işlemleri için kullanılacak
    public interface IReadRepsitory<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T > GetAll(); //Tüm verileri getir
        IQueryable<T> GetWhere(Expression<Func<T , bool>> method); //Belirli bir koşula göre verileri getir
        Task<T> GetSingleAsync(Expression<Func<T , bool>> method); //Belirli bir koşula göre tek bir veri getir
        Task<T> GetByIdAsync(string id); //Id'ye göre veri getir

    }
}
