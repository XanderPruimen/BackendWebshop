using Authentication.Data;
using Authentication.Models;
using Authentication.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext db;
        TokenController TC = new TokenController();
        public AccountController(DataContext db)
        {
            this.db = db;
        }

        [Route("/[controller]/login")]
        [HttpPost]
        public string login([FromHeader] string Authorization, [FromBody] User u)
        {
            string validToken = "";

            //check if exists
            var user = db.Users.Where(x => x.Email.Equals(u.Email) && x.Password.Equals(u.Password)).FirstOrDefault();

            string json = JsonConvert.SerializeObject(user);

            if (json == "[]")
            {
                return "Niet gevonden";
            }
            else
            {
                if (user == null)
                {
                    return null;
                }
                UserDTO uDTO = new UserDTO();
                //wel gevonden valideren
                if (Authorization == null)
                {
                    Authorization = "Invalid";
                }

                if (TC.isExpired(Authorization))
                {
                    //als token niet valid is
                    //redirect naar login
                    validToken = loginNoToken(user.Email, user.Role);
                    uDTO.Email = u.Email;
                    uDTO.Token = validToken;
                    return JsonConvert.SerializeObject(uDTO);
                }
                else
                {
                    //Als token wel valid is log meteen in
                    uDTO.Email = u.Email;
                    uDTO.Token = Authorization;
                    return JsonConvert.SerializeObject(uDTO);
                }
            }
        }
        public string loginNoToken(string Email, int role)
        {
            string validToken = TC.nonExistentToken(Email, role);

            return validToken;
        }

        [Route("/[controller]/register")]
        [HttpPost]
        public User register([FromBody] User u)
        {
            if (u.Email == "" || u.Username == "" || u.Password == "")
            {
                return null;
            }
            else
            {
                var user = db.Users.Where(x => x.Email.Equals(u.Email)).FirstOrDefault();

                if (user == null)
                {
                    try
                    {
                        db.Users.Add(u);
                        db.SaveChanges();
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

        [Authorize]
        [HttpGet]
        [Route("/[controller]/getUser")]
        public User getUser([FromHeader] string Authorization)
        {
            var x = TC.readOut(Authorization);
            User user = db.Users.Where(x => x.Email.Equals(x.Email)).FirstOrDefault();
            return user;
        }
    }
}
