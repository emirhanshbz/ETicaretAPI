using Microsoft.AspNetCore.Http;

namespace ETicaretAPI.Application.Abstractions.Storage
{
    //bütün storage servisleri için ortak interface
    public interface IStorage
    {
        Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files); //Ortak upload. yüklediğimiz dosyanın adını ve pathini tutuyoruz.
        Task DeleteAsync(string pathOrContainerName, string fileName);

        List<string> GetFiles(string pathOrContainerName);
        bool hasFile(string pathOrContainerName, string fileName);
    }
}
