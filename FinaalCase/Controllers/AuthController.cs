using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinalCaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private string GenerateJSONWebToken(int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysuperdupersecret"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var claims = new List<Claim>();
            if (userId == 1)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));

                //claims.Add(new Claim("Admin", "Admin"));
            }
            else if (userId == -1)
            {
                claims = null;
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Customer"));

                //claims.Add(new Claim("Customer", "Customer"));
            }

            var token = new JwtSecurityToken(
                                    issuer: "mySystem",
                                    audience: "myUsers",
                                    claims: claims,
                                    expires: DateTime.Now.AddMinutes(10),
                                    signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        [HttpGet("{userId}")]
        public ActionResult<string> Get(int userId)
        {
            var token = GenerateJSONWebToken(userId);

            return token == null ? null : token;
        }
    }
}
