﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EverGreenWebApi.Models;

namespace EverGreenWebApi.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        UserModel Login(string phonenumber);
        ResponseStatus Login(string phoneNumber,int loginId);
        UserModel Login(string phoneNumber, int otp, int loginId);
        //UserModel UpdateProfile(UserModel user);
        //UserModel GetUserProfile(int loginid);
        //UserModel IsMobileVerified(int loginid);
        //UserModel SocialUserLogin(UserModel user);
    }

}