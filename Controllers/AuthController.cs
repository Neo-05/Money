using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoneyApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoneyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //injection dépendance
        private readonly JwtOptions _jwtOption;
        public AuthController(JwtOptions jwtoptions)
        {
            _jwtOption = jwtoptions;
        }

        [HttpPost]
        public IActionResult Login(LoginModel lm)
        {
            if (lm == null) //faire les tests pr savoir directement 
            {
                return new NotFoundResult();
            }
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            if(lm.Pseudo!="Admin" || lm.Password != "test")
            {
                return new BadRequestObjectResult(lm);
            }

            //Générer token et renvoyer
            //Issuer
            byte[] skey = Encoding.UTF8.GetBytes(_jwtOption.SigningKey);
            SymmetricSecurityKey cle = new SymmetricSecurityKey(skey);

            //Claims
            Claim InfoNom = new Claim(ClaimTypes.Name, "Admin");
            Claim Rols = new Claim(ClaimTypes.Role, "Admin");

            //Token
            List<Claim> mesClaims = new List<Claim>();
            mesClaims.Add(InfoNom);
            mesClaims.Add(Rols);

            JwtSecurityToken Token = new JwtSecurityToken(

                issuer: _jwtOption.Issuer,
                audience: _jwtOption.Audience,
                claims: mesClaims,
                expires: DateTime.Now.AddSeconds(_jwtOption.Expiration),
                signingCredentials: new SigningCredentials(cle, SecurityAlgorithms.HmacSha256)
            );

            string tokenARenvoyer = new JwtSecurityTokenHandler().WriteToken(Token);
            return Ok(new { Nom=lm.Pseudo, Token=tokenARenvoyer  });

        }
    }
}
