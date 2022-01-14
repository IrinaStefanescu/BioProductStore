﻿using BioProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Utilities.JWTUtils
{
    public interface IJWTUtils
    {
        public string GenerateJWTToken(User user);
        public Guid ValidateJWTToken(string token);
    }
}
