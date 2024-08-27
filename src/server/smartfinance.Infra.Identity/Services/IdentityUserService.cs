using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using smartfinance.Domain.Common;
using smartfinance.Domain.Interfaces.Repositories;
using smartfinance.Domain.Interfaces.Services.Authentication;
using smartfinance.Domain.Models.Authentication.Create;
using smartfinance.Domain.Models.Authentication.Model;
using smartfinance.Infra.Identity.Configurations;
using smartfinance.Infra.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace smartfinance.Infra.Identity.Services
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly IUserValidator<AppIdentityUser> _userValidator;
        private readonly IEmailService _emailService;
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;

        public IdentityUserService(SignInManager<AppIdentityUser> signInManager,
                              UserManager<AppIdentityUser> userManager,
                              IUserValidator<AppIdentityUser> userValidator,
                              IOptions<JwtOptions> jwtOptions,
                              IEmailService emailService,
                              IAccountRepository accountRepository,
                              IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userValidator = userValidator;
            _jwtOptions = jwtOptions.Value;
            _emailService = emailService;
            _accountRepository = accountRepository;
            _configuration = configuration;
        }

        public async Task<OperationResult<bool>> ValidateUserAsync(AppIdentityUserViewModel model)
        {
            var appIdentityUser = new AppIdentityUser
            {
                Id = model.Id,
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await _userValidator.ValidateAsync(_userManager, appIdentityUser);

            if (!result.Succeeded)
            {
                return OperationResult<bool>.Failed(
                    result.Errors.Select(x => new Error(x.Code, x.Description)).ToList());
            }

            return OperationResult<bool>.Succeeded();
        }

        public async Task<OperationResult<bool>> ConfirmEmail(ConfirmEmailViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return OperationResult<bool>.Failed("Usuário não encontrado.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, model.Token);

            if (!result.Succeeded)
            {
                return OperationResult<bool>.Failed(
                   result.Errors.Any()
                   ? result.Errors.Select(x => new Error(x.Code, x.Description)).ToList()
                   : [], "Não foi possível confirmar o email de cadastro.");
            }

            return OperationResult<bool>.Succeeded();
        }

        public async Task<OperationResult<IdentityUserViewModel>> Login(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (result.Succeeded)
                return await GenerateCredentials(model.Email);

            string message = string.Empty;

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    message = "Essa conta está bloqueada";
                else if (result.IsNotAllowed)
                    message = "Essa conta não tem permissão para fazer login";
                else if (result.RequiresTwoFactor)
                    message = "É necessário confirmar o login no seu segundo fator de autenticação";
                else
                    message = "Usuário ou senha estão incorretos";
            }

            return OperationResult<IdentityUserViewModel>.Failed(message);
        }

        public async Task<OperationResult<IdentityUserViewModel>> LoginWithouPassword(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            string message = string.Empty;

            if (await _userManager.IsLockedOutAsync(user))
                message = "Essa conta está bloqueada";
            else if (!await _userManager.IsEmailConfirmedAsync(user))
                message = "Essa conta precisa confirmar seu e-mail antes de realizar o login";

            if (!string.IsNullOrWhiteSpace(message))
                return OperationResult<IdentityUserViewModel>.Failed(message);

            return await GenerateCredentials(user.Email);

        }

        public async Task<OperationResult<bool>> Register(CreateIdentityUserViewModel model)
        {
            var appIdentityUser = new AppIdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(appIdentityUser, model.Password);

            if (!result.Succeeded)
            {
                return OperationResult<bool>.Failed(
                    result.Errors.Any()
                    ? result.Errors.Select(x => new Error(x.Code, x.Description)).ToList()
                    : [], "Não foi possível criar o usuário.");
            }

            await _userManager.SetLockoutEnabledAsync(appIdentityUser, false);

            return OperationResult<bool>.Succeeded();
        }

        private async Task<OperationResult<IdentityUserViewModel>> GenerateCredentials(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var accessTokenClaims = await GetClaims(user, addClaimsUser: true);
            var refreshTokenClaims = await GetClaims(user, addClaimsUser: false);

            var expirationDateAccessToken = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);
            var expirationDateRefreshToken = DateTime.Now.AddSeconds(_jwtOptions.RefreshTokenExpiration);

            var accessToken = GenerateJwtToken(accessTokenClaims, expirationDateAccessToken);
            var refreshToken = GenerateJwtToken(refreshTokenClaims, expirationDateRefreshToken);

            var model = new IdentityUserViewModel()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return OperationResult<IdentityUserViewModel>.Succeeded(model);

        }

        private async Task<IList<Claim>> GetClaims(AppIdentityUser user, bool addClaimsUser)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString())
            };

            if (addClaimsUser)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                claims.AddRange(userClaims);

                foreach (var role in roles)
                    claims.Add(new Claim("role", role));
            }

            return claims;
        }

        private string GenerateJwtToken(IEnumerable<Claim> claims, DateTime expirationDate)
        {
            var jwt = new JwtSecurityToken(
               issuer: _jwtOptions.Issuer,
               audience: _jwtOptions.Audience,
               claims: claims,
               notBefore: DateTime.Now,
               expires: expirationDate,
               signingCredentials: _jwtOptions.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }

        public async Task<OperationResult<bool>> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            var account = await _accountRepository.FindByEmailAsync(model.Email);

            if (account == null || user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return OperationResult<bool>.Failed("Não foi recuperar o senha.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string baseUrl = _configuration["Application:BaseHost"];

            string resetPasswordUrl = $"{baseUrl}/account/reset-password?token={token}";

            var _body = $"<p>Olá, {account.Name} esqueceu sunha senha?, para cadastrar uma nova senha acesse o link: {resetPasswordUrl}</p>";

            await _emailService.SendEmailAsync(model.Email, "Recuperação de senha", _body, true);

            return OperationResult<bool>.Succeeded();
        }

        public async Task<OperationResult<bool>> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            var account = await _accountRepository.FindByEmailAsync(model.Email);

            if (account == null || user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return OperationResult<bool>.Failed("Não foi recuperar o senha.");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.ResetToken, model.Password);

            if (!result.Succeeded)
            {
                return OperationResult<bool>.Failed(
                    result.Errors.Any()
                    ? result.Errors.Select(x => new Error(x.Code, x.Description)).ToList()
                    : [], "Não foi alterar a senha do usuário.");
            }

            return OperationResult<bool>.Succeeded();
        }
    }
}
