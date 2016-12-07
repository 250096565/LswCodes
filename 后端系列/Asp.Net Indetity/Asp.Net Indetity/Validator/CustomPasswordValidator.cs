using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Asp.Net_Indetity.Validator
{
    /// <summary>
    /// 自定义密码验证
    /// </summary>
    public class CustomPasswordValidator : PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string password)
        {
            IdentityResult result = await base.ValidateAsync(password);
            if (password.Contains("123456"))
            {
                List<string> errors = result.Errors.ToList();
                errors.Add("密码不能包含连续数字");
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}