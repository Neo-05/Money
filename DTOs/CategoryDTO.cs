using System.ComponentModel.DataAnnotations;

namespace MoneyApi.DTOs
{
    public class CategoryDTO
    {
        [Required]
        public int Id_Category { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }


    //Sans Id
    public class CategoryDataDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
