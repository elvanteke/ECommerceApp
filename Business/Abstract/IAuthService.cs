﻿using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        User Register(UserForRegisterDto userForRegisterDto, string password);
        User Login(UserForLoginDto userForLoginDto);
        bool UserExists(string email);
        AccessToken CreateAccessToken(User user);
    }
}
