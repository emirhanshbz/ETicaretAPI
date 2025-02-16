using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    public interface IWriteRepository<T> :IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model); //Veri ekle
        Task<bool> AddRangeAsync(List<T> datas); //Çoklu veri ekle
        bool Remove(T model); //Veri sil
        bool RemoveRange(List<T> datas); //Çoklu veri sil
        Task<bool> RemoveAsync(string id); //Id'ye göre veri sil
        bool Update(T model); //Veri güncelle
        Task<int> SaveAsync(); //Değişiklikleri kaydet
    }
}
