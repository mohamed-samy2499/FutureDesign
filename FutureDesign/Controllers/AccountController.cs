﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Helper;
using Project.DAL.Entities;
using Project.PL.Models;
using System.Threading.Tasks;

namespace Project.PL.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }
        #region Constructor
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        #endregion 
        #region Sign Up

        public async Task<IActionResult> RegisterAsync(RegisterViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        #endregion
        #region Sign In
        public IActionResult Login() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, true);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Email");
            }
            return View(model);
        }
        #endregion
        #region Forgot Password
        public IActionResult ForgotPassword() 
        {
            return View();
        }
        public IActionResult CompleteForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //send an email with the link to reset page
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var Token = await UserManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { Email = user.Email, Token = Token }, Request.Scheme);
                    var email = new Email()
                    {
                        Title = "Reset Password",
                        Body = ResetPasswordLink
                    };
                    EmailSettings.SendEmail(email, user);
                    return RedirectToAction(nameof(CompleteForgotPassword));
                }
            }
            return View(model);
        }
        #endregion
        #region Reset Password
        public IActionResult ResetPassword()
        {
            string Email = Request.Query["Email"].ToString();
            string Token = Request.Query["Token"].ToString();
            ViewData["token"] = Token;
            ViewData["email"] = Email;

            return View(new ResetPasswordViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = null;
                if (model.Email != null && model.Token != null)
                {

                    user = await UserManager.FindByEmailAsync(model.Email);
                }
                if (user != null)
                {
                    var result = await UserManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(ResetPasswordDone));
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        public IActionResult ResetPasswordDone()
        {
            return View();
        }
        #endregion

        #region Sign Out
        public async new Task<IActionResult> SignOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
    }
}
