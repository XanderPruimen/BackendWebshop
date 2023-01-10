using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Models
{
    public class User
    {
        [Key]
        public int userID { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int role { get; set; }
    }
}
