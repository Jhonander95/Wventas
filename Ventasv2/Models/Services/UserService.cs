using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ventasv2.Models.Common;
using Ventasv2.Models.Request;
using Ventasv2.Models.Response;
using Ventasv2.Tools;

namespace Ventasv2.Models.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public UserResponse Auth(AuthRequest model)
        {
            UserResponse userresponse = new UserResponse();
            using (var db = new VentaRealContext())
            {
                //string spassword = Encrypt.GetSHA256(Encrypt.GetSHA256(model.Password));

                //var usuario = db.Usuarios.Where(d => d.Email == model.Email &&
                //                              d.Password == spassword).FirstOrDefault();

                // if(usuario == null) return null;

                //userresponse.Email = usuario.Email;

                var usuario = db.Usuarios.Where(d => d.Email == model.Email &&
                                                d.Password == model.Password).FirstOrDefault();
                 if(usuario == null) return null;

                userresponse.Email = usuario.Email;
                userresponse.Token = GetToken(usuario);
            }

            return userresponse;

        }

        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Email.ToString())
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = 
                new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
