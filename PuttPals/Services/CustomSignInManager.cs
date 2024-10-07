using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

public class CustomSignInManager<TUser> : SignInManager<TUser> where TUser : class
{
    public CustomSignInManager(UserManager<TUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<TUser> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<TUser>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<TUser> confirmation)
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
    }

    public override async Task<SignInResult> PasswordSignInAsync(string userNameOrEmail, string password,
        bool isPersistent, bool lockoutOnFailure)
    {
        TUser user;

        // Check if the input is an email
        if (userNameOrEmail.Contains("@"))
        {
            user = await UserManager.FindByEmailAsync(userNameOrEmail);
        }
        else
        {
            user = await UserManager.FindByNameAsync(userNameOrEmail);
        }

        if (user == null)
        {
            return SignInResult.Failed;
        }

        return await PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
    }
}
