using System.ComponentModel.DataAnnotations;

namespace MoneyApi.Models
{
    public class LoginModel
    {
        private string _pseudo;
        private string _password;

        public string Pseudo { get => _pseudo; set => _pseudo = value; }

        public string Password { get => _password; set => _password = value; }
    }
}