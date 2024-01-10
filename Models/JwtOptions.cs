namespace MoneyApi.Models
{
    // le record définit un type préférence
    public record class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; }
        public int Expiration { get; set; }
    }
}
