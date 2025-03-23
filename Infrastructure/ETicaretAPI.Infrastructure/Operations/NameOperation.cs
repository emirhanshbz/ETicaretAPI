using System.Text.RegularExpressions;

namespace ETicaretAPI.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
            => Regex.Replace(
                Regex.Replace(
                    name.ToLower()
                        .Replace("ö", "o").Replace("ü", "u").Replace("ğ", "g")
                        .Replace("ş", "s").Replace("ç", "c").Replace("ı", "i")
                        .Replace("İ", "i").Replace("Ö", "o").Replace("Ü", "u")
                        .Replace("Ğ", "g").Replace("Ş", "s").Replace("Ç", "c")
                        .Replace("â", "a").Replace("î", "i").Replace("û", "u")
                        .Replace("ê", "e").Replace("ä", "a").Replace("ë", "e")
                        .Replace("ï", "i").Replace("ô", "o").Replace("à", "a")
                        .Replace("è", "e"),
                    "[^a-z0-9-]", "-"), // Özel karakterleri kaldır, sadece a-z, 0-9 ve - bırak
                "-{2,}", "-") // Birden fazla tireyi tek bir tire yap
                .Trim('-'); // Baş ve sondaki tireleri temizle
    }
}
