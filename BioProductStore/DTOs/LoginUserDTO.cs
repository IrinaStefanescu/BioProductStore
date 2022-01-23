using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.DTOs
{
    public class LoginUserDTO
    {
        //data sent by us to login

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
