using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Identity.Jwt;
using NetDevPack.Identity.Model;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using BomTratoApp.Services.Interfaces;
using BomTratoApp.Models;

namespace BomTratoApi.Controllers
{
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppJwtSettings _appJwtSettings;
        private readonly IAprovadorService _aprovadorService;
        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppJwtSettings> appJwtSettings,
            IAprovadorService aprovadorService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appJwtSettings = appJwtSettings.Value;
            _aprovadorService = aprovadorService;
        }
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterUser registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(registerUser);
            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };
            var aprovador = await _aprovadorService.GetByEmail(registerUser.Email);
            if(aprovador is null)
            {
                AddError("Não há colaborador cadastrado com esse email");
                return CustomResponse();
            }
            var result = await _userManager.CreateAsync(user, registerUser.Password);            
            var registeredUser = GetFUllUser(registerUser.Email, aprovador);
            if (result.Succeeded) return CustomResponse(registeredUser);
            foreach (var error in result.Errors)
            {
                AddError(error.Description);
            }
            return CustomResponse();
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);
            if (result.Succeeded)
            {
                var aprovador = await _aprovadorService.GetByEmail(loginUser.Email);
                var user = GetFUllUser(loginUser.Email, aprovador);
                return CustomResponse(user);
            }
            if (result.IsLockedOut)
            {
                AddError("This user is temporally blocked");
                return CustomResponse();
            }
            AddError("Incorrect User or Password");
            return CustomResponse();
        }
        private UserLoginViewModel GetFUllUser(string email, AprovadorViewModel aprovador)
        {
            return new UserLoginViewModel()
            {
                Jwt = GetFullJwt(email),
                User = GetUser(aprovador)
            };
        }
        private static UserViewModel GetUser(AprovadorViewModel aprovador)
        {
            return new UserViewModel()
            {
                Id = aprovador.Id,
                Email = aprovador.Email,
                Name = aprovador.Name
            };
        }
        private string GetFullJwt(string email)
        {
            return new JwtBuilder()
                .WithUserManager(_userManager)
                .WithJwtSettings(_appJwtSettings)
                .WithEmail(email)
                .BuildToken();
        }
    }
}
