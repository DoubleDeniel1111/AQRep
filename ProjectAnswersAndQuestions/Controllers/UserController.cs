using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectAnswersAndQuestions.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using ProjectAnswersAndQuestions.DAL;
using Microsoft.EntityFrameworkCore;
using ProjectAnswersAndQuestions.DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.IO;
using System.Web.Helpers;
using ProjectAnswersAndQuestions.Services;

namespace ProjectAnswersAndQuestions.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService userService;
        private readonly IRoleService roleService;


        public UserController(IUserService userService,IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                UserRegistration _user = await userService.GetUserByData(user);
                if (_user != null)
                {

                    await Authenticate(user.Email);                 
                    return RedirectToAction("AQpage", "AQpage");
                }
                else
                {
                    ViewBag.ErrorText = "Invalid account email or password";
                    return View();
                }
            }
          
            return View();
        }     
        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(UserRegistration user)
        {
            if (ModelState.IsValid)
            {
                UserRegistration _user = await userService.GetUserByData(user);
                if (_user != null)
                {
                    ViewBag.ErrorText = "AQ account with a such mail already exists";
                    return View();
                }
                else
                {
                    
                    if (user.FileAvatar != null)
                    {
                        byte[] imageData = null;
                        // считываем переданный файл в массив байтов
                        using (var binaryReader = new BinaryReader(user.FileAvatar.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)user.FileAvatar.Length);
                        }
                        // установка массива байтов
                        user.Avatar = imageData;
                    }
                    user.RoleID = await roleService.SetRole();
                    await userService.AddUser(user);
                    await Authenticate(user.Email);

                    var verifEmailService = new VerifEmailService();
                    verifEmailService.SendConfirmationEmail(user);
                    return RedirectToAction("Login", "User");
                }
            }
            return View();
        }

        private async Task Authenticate(string userEmail)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userEmail)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }



    }
}
