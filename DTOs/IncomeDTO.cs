using System.ComponentModel.DataAnnotations;

namespace MoneyApi.DTOs
{
    public class IncomeDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string Currency { get; set; } = string.Empty;
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int PeopleId { get; set; }
    }
    //Sans Id
    public class IncomeDataDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string Currency { get; set; } = string.Empty;
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int PeopleId { get; set; }
    }
}
