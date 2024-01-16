using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Money.BLL.Services;
using Money.DAL.Entities;
using MoneyApi.DTOs;
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
        private readonly AuthService _AuthService;

        public AuthController(JwtOptions jwtoptions, AuthService authService)
        {
            _jwtOption = jwtoptions;
            _AuthService = authService;
        }
        
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401, Type = typeof(string))]
        public IActionResult Login([FromBody]LoginModel lm)
        {
            if (lm == null) //faire les tests pr savoir directement 
            {
                return new NotFoundResult();
            }
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            int peopleId = _AuthService.Login(lm.Pseudo, lm.Password);

            if (peopleId != 0)
            {
                byte[] skey = Encoding.UTF8.GetBytes(_jwtOption.SigningKey);
                SymmetricSecurityKey cle = new SymmetricSecurityKey(skey);

                //Claims
                Claim idPeople = new Claim("id", peopleId.ToString());
                List<Claim> mesClaims = new List<Claim>
                {
                    idPeople
                };

                //Token
                JwtSecurityToken Token = new JwtSecurityToken(
                    issuer: _jwtOption.Issuer,
                    audience: _jwtOption.Audience,
                    claims: mesClaims,
                    expires: DateTime.Now.AddSeconds(_jwtOption.Expiration),
                    signingCredentials: new SigningCredentials(cle, SecurityAlgorithms.HmacSha256));

                string tokenARenvoyer = new JwtSecurityTokenHandler().WriteToken(Token);
                return Ok(new { Nom = lm.Pseudo, Token = tokenARenvoyer });
            }
            else
            {
                return Unauthorized("Non autorisé");
            }
        }
    }
}
