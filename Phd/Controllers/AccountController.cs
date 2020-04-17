﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phd.ViewModels;
using Phd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Phd.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly PhdContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, PhdContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        [HttpGet]
        public IActionResult Register()
        {

            var faculties = _context.Faculty.ToList();
            var departments = _context.AcademicDepartment.Include(p => p.Faculty).Include(p => p.User).AsEnumerable();
            //var allDeps = _context.AcademicDepartment.ToList();
            //var depsByFaculty = from d in departments
            //                    where d.FacultyId == facultyId
            //                    select d;
            //departments = departments.Where(d => d.Faculty.Id == facultyId);



            // модифицирую регистрацию с добавлением ГАК

            ViewData["FacultyId"] = new SelectList(_context.Faculty, "Id", "Name");
            //ViewData["AcademicDepartmentId"] = new SelectList(_context.AcademicDepartment, "Id", "Name");
            ViewBag.Faculties = faculties;
            //ViewData["AcademicDepartmentId"] = new SelectList(_context.AcademicDepartment.Where(d => d.FacultyId == id), "Id", "Name");
            ViewData["BMajorId"] = new SelectList(_context.BMajor, "Id", "Cypher");
            ViewData["BRExamCommissionId"] = new SelectList(_context.BRExamCommission, "Id", "Name");
            

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                User user = new User 
                { 
                    Email = model.Email,
                    UserName = model.Email,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    AcademicDepartmentId = model.AcademicDepartmentId,
                    BMajorId = model.BMajorId,
                    BRExamCommissionId = model.BRExamCommissionId,
                    FacultyId = model.FacultyId
                };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // генерация токена для пользователя
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                    // return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
                    return RedirectToAction("RegistrationSend", "Account");
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


        [HttpGet]
        public IActionResult RegisterModerator()
        {
            // модифицирую регистрацию с добавлением ГАК
            //return View(); 
            //

            ViewData["FacultyId"] = new SelectList(_context.Faculty, "Id", "Name");
            ViewData["AcademicDepartmentId"] = new SelectList(_context.AcademicDepartment, "Id", "Name");
            ViewBag.BMajorId = 1;
            ViewBag.BRExamCommissionId = 1;


            return View();
        }


        public async Task<IActionResult> RegisterModerator(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    FacultyId = model.FacultyId,
                    AcademicDepartmentId = model.AcademicDepartmentId,
                    BMajorId = model.BMajorId,
                    BRExamCommissionId = model.BRExamCommissionId
                    
                };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // генерация токена для пользователя
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                    // return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
                    return RedirectToAction("RegistrationSend", "Account");
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



























        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("RegistrationConfirm", "Account");
            else
                return View("Error");
        }




        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            ///////////////////////
           

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    // проверяем, подтвержден ли email

                        if (!await _userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                            return View(model);
                        }


                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);

         }



           // [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }







        // восстановление пароля

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // пользователь с данным email может отсутствовать в бд
                    // тем не менее мы выводим стандартное сообщение, чтобы скрыть 
                    // наличие или отсутствие пользователя в бд
                    return View("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(model.Email, "Reset Password",
                    $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a>");
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }




        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return View("ResetPasswordConfirmation");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }




        [HttpGet]
        public IActionResult RegistrationConfirm()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegistrationSend()
        {
            return View();
        }




    }
}