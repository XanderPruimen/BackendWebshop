using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Text;
using BackendWebshop.DTO_s;
using System.Security.Principal;
using BackendWebshop.Models;

namespace BackendWebshop.Controllers
{

    public class TokenController  
    {
        private const string SECRET_KEY = "this is my custom Secret key for authnetication";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenController.SECRET_KEY));

        public object GenerateToken(UserDTO user)
        {
            var token = new JwtSecurityToken(
                claims: new Claim[]
                {
                    new Claim("AccountID", Convert.ToString(user.AccountID)),
                    new Claim("Email", user.Email),
                    new Claim("Username", user.Username)
                },
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(60),
                signingCredentials: new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Authorize]
        public bool isExpired(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return false;
            }
            catch
            {
                return true;
            }
        }

        public bool IsTokenExpired(string token)
        {
            if (token == null || ("").Equals(token))
            {
                return true;
            }

            /***
             * Make string valid for FromBase64String
             * FromBase64String cannot accept '.' characters and only accepts stringth whose length is a multitude of 4
             * If the string doesn't have the correct length trailing padding '=' characters should be added.
             */
            int indexOfFirstPoint = token.IndexOf('.') + 1;
            String toDecode = token.Substring(indexOfFirstPoint, token.LastIndexOf('.') - indexOfFirstPoint);
            while (toDecode.Length % 4 != 0)
            {
                toDecode += '=';
            }

            //Decode the string
            string decodedString = Encoding.ASCII.GetString(Convert.FromBase64String(toDecode));

            //Get the "exp" part of the string
            Regex regex = new Regex("(\"exp\":)([0-9]{1,})");
            Match match = regex.Match(decodedString);
            long timestamp = Convert.ToInt64(match.Groups[2].Value);

            DateTime date = new DateTime(1970, 1, 1).AddSeconds(timestamp);
            DateTime compareTo = DateTime.UtcNow;

            int result = DateTime.Compare(date, compareTo);

            return result < 0;
        }

        public List<Claim> readOutToken(string test)
        {
            string[] tokentemp = test.Split(" ");
            List<Claim> data = new List<Claim>();

            var token = tokentemp[1];
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            foreach (Claim c in jwtSecurityToken.Claims)
            {
                data.Add(c);
            }
            return data;
        }

        public User TokenToUser(string token)
        {
            try
            {
                List<Claim> data = new List<Claim>();

                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);

                foreach (Claim c in jwtSecurityToken.Claims)
                {
                    data.Add(c);
                }

                User user = new User();
                user.AccountID = Convert.ToInt32(data.FirstOrDefault(c => c.Type == "AccountID").Value);
                user.Email = data.FirstOrDefault(c => c.Type == "Email").Value;
                user.Username = data.FirstOrDefault(c => c.Type == "Username").Value;

                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}
