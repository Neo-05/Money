using System.ComponentModel.DataAnnotations;

namespace MoneyApi.DTOs
{
    public class ProjectDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int SpareAmount { get; set; }
        [Required]
        public string Currency { get; set; } = string.Empty;
        [Required]
        public int PeopleId { get; set; }
    }
    //Sans Id
    public class ProjectDataDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int SpareAmount { get; set; }
        [Required]
        public string Currency { get; set; } = string.Empty;
        [Required]
        public int PeopleId { get; set; }
    }
}
