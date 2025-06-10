using ETicaretAPI.Domain.Entities.Common;

namespace ETicaretAPI.Domain.Entities
{
    public class Menu : BaseEntity 
    {
        public string Name { get; set; }

        public ICollection<Endpoint> EndPoints { get; set; }
    }
}
