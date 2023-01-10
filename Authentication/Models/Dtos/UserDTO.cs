using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Models.Dtos
{
    public class UserDTO
    {
        public string email { get; set; }
        public string token { get; set; }
    }
}
