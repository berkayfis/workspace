using CustomAuthentication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace CustomAuthentication.Services
{
    public class UserService
    {
        private readonly IMongoCollection<UserModel> _users;

        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("FinalProjectDB"));
            var database = client.GetDatabase("FinalProjectDB");
            _users = database.GetCollection<UserModel>("Users");
        }

        public List<UserModel> GetAllUsers()
        {
            return _users.Find(user => true).ToList();
        }

        public UserModel GetUserByUsername(string username)
        {
            return _users.Find<UserModel>(user => user.UserName == username).FirstOrDefault();
        }

        public UserModel GetUserByEmail(string email)
        {
            return _users.Find<UserModel>(user => user.Email == email).FirstOrDefault();
        }

        public UserModel CreateUser(UserModel user)
        {
            var userExist = ValidateUsernameAndEmail(user);
            if (userExist == null)
            {
                _users.InsertOne(user);
                return null;
            }            
            return userExist;
        }

        public UserModel ValidateUsernameAndEmail(UserModel user)
        {
            if (GetUserByEmail(user.Email) == null)
            {
                 if(GetUserByUsername(user.UserName) == null)
                {
                    return null;
                }
                else
                {
                    return GetUserByUsername(user.UserName);
                }
            }
            else
            {
                return GetUserByEmail(user.Email);
            }            
        }


        //private async System.Threading.Tasks.Task<bool> isAdminAsync(UserModel user)
        //{
        //    if (user.UserName == "Admin" && user.Password == "Admin")
        //    {

        //        var identity = new ClaimsIdentity(new[] {
        //            new Claim(ClaimTypes.Name, user.UserName)
        //        }, CookieAuthenticationDefaults.AuthenticationScheme);

        //        var principal = new ClaimsPrincipal(identity);

        //        var login = await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


        //        return true;
        //    }

        //    return false;
        //}
    }
}
