using System.ComponentModel.DataAnnotations;

namespace MoneyApi.DTOs
{
    public class PeopleDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Pseudo { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        public string HashPwd { get; set; } = string.Empty;
    }


    //Pas besoin de l'id car créé dans la db
    public class PeopleDataDTO
    {

        [Required] //oblige à valider les données entrées
        public string Pseudo { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        public string HashPwd { get; set; } = string.Empty;
    }
}
