using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Models
{
    public class IdentitySeedData
    {
			private const string Login = "Admin";
			private const string Password = "Admin123!";

			public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
			{
					IdentityUser user = await userManager.FindByIdAsync(Login);
					
				if (user == null)
				{
				user = new IdentityUser("Admin");
				await userManager.CreateAsync(user, Password);
				}
		}
		
	}
}
