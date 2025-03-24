using F = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Repositories
{
    public interface IFileReadRepository : IReadRepsitory<F::File> //farklı bir yol tür belirtmek için
    {
    }
}
