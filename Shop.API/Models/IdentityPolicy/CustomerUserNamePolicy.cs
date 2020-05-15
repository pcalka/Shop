using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.API.Models.IdentityPolicy
{
    public class CustomerUserNamePolicy : UserValidator<AppUser>
    {
        public override async Task<IdentityResult> ValidateAsync
            (UserManager<AppUser> userManager, AppUser user)
        {
            IdentityResult result = await base.ValidateAsync(userManager, user);
            List<IdentityError> errors = result.Succeeded ?
                new List<IdentityError>() : result.Errors.ToList();
            if (!user.Email.ToLower().EndsWith("@yahoo.com"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Only yahoo.com email addresses are allowed"
                });
            }
            return errors.Count == 0 ? IdentityResult.Success : 
            IdentityResult.Failed(errors.ToArray());           
        }
    }
}
