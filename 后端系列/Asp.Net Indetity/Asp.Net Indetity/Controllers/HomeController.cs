using Asp.Net_Indetity.Manager;
using Asp.Net_Indetity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Asp.Net_Indetity.Controllers
{
    public class HomeController : Controller
    {
        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }

        // GET: Home
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Name, Email = model.Email };
                //传入Password并转换成PasswordHash
                IdentityResult result = await UserManager.CreateAsync(user,
                    model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                AddErrorsFromResult(result);
            }
            return View(model);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View("Error", result.Errors);
            }
            return View("Error", new[] { "User Not Found" });
        }


        /// <summary>
        /// 修改 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Edit(string id, string email, string password)
        {
            //根据Id找到AppUser对象
            AppUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                    //验证密码是否满足要求
                    validPass = await UserManager.PasswordValidator.ValidateAsync(password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                //验证Email是否满足要求
                user.Email = email;
                IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "无法找到改用户");
            }
            return View(user);
        }

        /// <summary>
        /// 添加错误信息
        /// </summary>
        /// <param name="result"></param>
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}