using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity //T türü BaseEntity'den türemiş olmak zorunda
    {
        DbSet<T> Table { get; }
    }
}
