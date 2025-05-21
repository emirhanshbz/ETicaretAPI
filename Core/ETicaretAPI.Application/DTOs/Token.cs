namespace ETicaretAPI.Application.DTOs
{
    public class Token  //DTOlar genellikle servisler arası veri iletiminde kullanılır.
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
