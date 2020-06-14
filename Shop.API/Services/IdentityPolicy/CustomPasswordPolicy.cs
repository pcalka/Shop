using Microsoft.AspNetCore.Identity;
using Shop.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.API.Services.IdentityPolicy
{
    public class CustomPasswordPolicy : PasswordValidator<AppUser>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<AppUser> userManager,
            AppUser user, string password)
        {
            IdentityResult result = await base.ValidateAsync(userManager, user, password);
            List<IdentityError> errors = result.Succeeded ? 
                new List<IdentityError>() : result.Errors.ToList();
            if (password.ToLower().Contains(user.UserName.ToLower()))
                { 
                    errors.Add(new IdentityError
                    {
                        Description = "Password can't contain username"
                    });
                }
            if(password.Contains("123"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Password cannot contain 123 numeric sequence"
                });
            }
            return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }
}
