using System.ComponentModel.DataAnnotations;

namespace MoneyApi.DTOs
{
    public class ExpenseDTO
    {
        [Required]
        public int Id_Expense { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string Currency { get; set; } = string.Empty;
        [Required]
        public int PeopleId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
    //Sans Id
    public class ExpenseDataDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string Currency { get; set; } = string.Empty;
        [Required]
        public int PeopleId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
