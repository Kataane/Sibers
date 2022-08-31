namespace Sibers.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IService service;
    private readonly UserManager<EmployeeUser> userManager;
    private readonly SignInManager<EmployeeUser> signInManager;

    public AccountController(IService service, UserManager<EmployeeUser> userManager, SignInManager<EmployeeUser> signInManager)
    {
        this.service = service;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    [HttpGet]
    [Route("current")]
    public async Task<IActionResult> Current()
    {
        var user = User.Identity?.Name;

        return user is null ? BadRequest() : Ok(user);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        try
        {
            var result = await signInManager.PasswordSignInAsync(
                model.Email, model.Password,
                model.IsPersistent, false);

            return result.Succeeded ? Ok() : BadRequest("Invalid password or email");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }


    [Authorize(Roles = $"{Roles.Lead}")]
    [HttpDelete]
    [Route("users/{userId}/roles/{role}")]
    public async Task<IActionResult> DeleteRole(string userId, string role)
    {
        try
        {
            var user = await userManager.FindByIdAsync(userId);
            await userManager.RemoveFromRoleAsync(user, role);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }


    [Authorize(Roles = $"{Roles.Lead}")]
    [HttpPost]
    [Route("users/{userId}/roles/{role}")]
    public async Task<IActionResult> AddRole(string userId, string role)
    {
        try
        {
            var user = await userManager.FindByIdAsync(userId);

            var roles = await userManager.GetUsersInRoleAsync(role);
            if (roles.Any(r => r.Id == userId)) return BadRequest($"User already have role {role}");

            await userManager.AddToRoleAsync(user, role);
            await userManager.AddClaimAsync(user, new Claim("Role", role));

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }
}

public class RegisterModel
{
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    public string Password { get; set; }
}

public class LoginModel
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
         
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool IsPersistent { get; set; }
}