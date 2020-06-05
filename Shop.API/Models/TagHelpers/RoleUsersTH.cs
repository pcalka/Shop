using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.API.Models.TagHelpers
{
    [HtmlTargetElement("td", Attributes ="i-role")]
    public class RoleUsersTH : TagHelper
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public RoleUsersTH(UserManager<AppUser> userManager,
         RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HtmlAttributeName("i-role")]
        public string Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context,
            TagHelperOutput output)
        {
            List<string> Names = new List<string>();
            IdentityRole RoleResult = await _roleManager.FindByIdAsync(Role);
            if(RoleResult != null)
            {
                foreach(var user in _userManager.Users)
                {
                    if (user != null && await _userManager.IsInRoleAsync(user, RoleResult.Name))
                        Names.Add(user.UserName);
                }
            }
            output.Content.SetContent(Names.Count == 0 ? "No users" : 
                string.Join(",", Names));
        }
    }
}
