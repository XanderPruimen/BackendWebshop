using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BackendWebshop.Context;
using BackendWebshop.DTO_s;
using BackendWebshop.Models;

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

        [Route("/[controller]/register")]
        [HttpPost]
        public User register([FromBody] User u)
        {
            if (u.Username == "" || u.Email == "" || u.Password == "")
            {
                return null;
            }
            else
            {
                var user = _dbContext.users.Where(x => x.Email.Equals(u.Email)).FirstOrDefault();

                if (user == null)
                {
                    try
                    {
                        _dbContext.users.Add(user);
                        _dbContext.SaveChanges();
                        return u;
                        //redirect naar login page.
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

        }


    }
}
