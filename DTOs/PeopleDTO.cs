using System.ComponentModel.DataAnnotations;

namespace MoneyApi.DTOs
{
    public class PeopleDTO
    {
        [Required] //DataAnnotations
        public int Id { get; set; }
        [Required]
        public string Pseudo { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
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
        public DateTime BirthDate { get; set; }
        [Required]
        public string HashPwd { get; set; } = string.Empty;
    }



















    //public class PeopleLogin
    //{
    //    private string _pseudo;
    //    private string _password;

    //    public string Pseudo { get => _pseudo; set => _pseudo = value; }

    //    public string Password { get => _password; set => _password = value; }
    //}
}
