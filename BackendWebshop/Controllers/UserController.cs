using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BackendWebshop.Context;
using BackendWebshop.DTO_s;
using BackendWebshop.Models;
using System.Security.Principal;

namespace BackendWebshop.Controllers
{

    public class UserController : ControllerBase
    {
        TokenController TC = new TokenController();
        private readonly ApplicationDBContext _dbContext;
        public UserController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("/[controller]/login")]
        [HttpPost]
        public async Task<IActionResult> Login(string AuthenticationToken, User loginUser)
        {
            UserDTO user = new UserDTO();

            try
            {
                if (string.IsNullOrEmpty(AuthenticationToken))
                {
                    //Check if account Exists
                    user = _dbContext.users.FirstOrDefault(a => a.Email == loginUser.Email && a.Password == loginUser.Password);
                    if (user.AccountID == 0)
                    {
                        return BadRequest("Account_Not_Found");
                    }
                    else
                    {
                        //Generate Token
                        AuthenticationToken = Convert.ToString(TC.GenerateToken(user));
                        return Ok(AuthenticationToken);
                    }
                }
                else
                {
                    //Check if token is expired 
                    if (TC.IsTokenExpired(AuthenticationToken))
                    {
                        AuthenticationToken = Convert.ToString(TC.GenerateToken(user));
                        return BadRequest("Token_Expired");
                    }
                    else
                    {
                        return Ok(AuthenticationToken);
                    }
                }
            }
            catch
            {
                return BadRequest("Error_When_Getting_Account");
            }
        }

        [HttpPost("/[controller]/register")]
        public async Task<IActionResult> Register(Register registerForm)
        {
            if (string.IsNullOrWhiteSpace(registerForm.Email) || string.IsNullOrWhiteSpace(registerForm.Email) || string.IsNullOrWhiteSpace(registerForm.Email))
            {
                return BadRequest("Missing_Data");
            }
            else
            {
                var user = _dbContext.users.Where(x => x.Email.Equals(registerForm.Email)).FirstOrDefault();

                if (user == null)
                {
                    try
                    {
                        UserDTO userDTO = new UserDTO()
                        {
                            Email = registerForm.Email,
                            Username = registerForm.Username,
                            Password = registerForm.Password,
                        };

                        _dbContext.users.Add(userDTO);
                        _dbContext.SaveChanges();
                        return Ok("User_Saved");
                    }
                    catch
                    {
                        return BadRequest("Error_While_Saving_User");
                    }
                }
                else
                {
                    return BadRequest("Email_Already_Used");
                }
            }

        }

        [Route("/[controller]/CheckToken")]
        [HttpPost]
        public async Task<IActionResult> CheckToken(string token)
        {
            try
            {
                //Check Input
                if (token == null)
                {
                    return BadRequest("Missing_Token");
                }

                //Check if token is expired
                if (TC.isExpired(token) == true)
                {
                    return BadRequest("Token_Expired_Or_Not_Valid");
                }

                //Get Account from Token
                User user = TC.TokenToUser(token);
                if (user.AccountID == 0)
                {
                    return BadRequest("No_Valid_AccountID");
                }

/*                //Refresh Token
                string newToken = TC.GenerateToken(user);
                Response.Headers.Add("AuthenticationToken", newToken);
*/
                return Ok(user);
            }
            catch
            {
                return BadRequest("Error_When_Getting_Account");
            }
        }


    }
}
