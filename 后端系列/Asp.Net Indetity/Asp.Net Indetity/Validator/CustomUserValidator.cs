using Asp.Net_Indetity.Manager;
using Asp.Net_Indetity.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Asp.Net_Indetity.Validator
{
    /// <summary>
    /// 自定义邮箱验证
    /// </summary>
    public class CustomUserValidator : UserValidator<AppUser>
    {
        public CustomUserValidator(AppUserManager mgr)
            : base(mgr)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);

            if (!user.Email.ToLower().EndsWith("@hotmail.com"))
            {
                List<string> errors = result.Errors.ToList();
                errors.Add("Email 地址只支持hotmail域名");
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}