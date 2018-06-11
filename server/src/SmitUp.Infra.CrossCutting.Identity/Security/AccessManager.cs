using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SmitUp.Infra.CrossCutting.Identity.Models;
using SmitUp.Infra.CrossCutting.Identity.Security.TokenConfig;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SmitUp.Infra.CrossCutting.Identity.Security
{
    public class AccessManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;

        public AccessManager(
                UserManager<User> userManager,
                SignInManager<User> signInManager,
                SigningConfigurations signingConfigurations,
                TokenConfigurations tokenConfigurations
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }
        public async Task<IdentityResult> CreateUser(User user)
        {
            return await _userManager.CreateAsync(user, user.PasswordHash);
        }

        public async Task<User> GetUser(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<string> GenerateEmailConfirmationToken(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailToken(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user,token);
        }

        public async Task<Token> ValidateCredentials(User user)
        {
            var userIdentity = await _userManager
                .FindByNameAsync(user.UserName);

            if (userIdentity != null)
            {
                var resultadoLogin = await _signInManager
                    .CheckPasswordSignInAsync(userIdentity, user.PasswordHash, false);               

                if (resultadoLogin.Succeeded)
                    return await GenerateToken(userIdentity);
            }

            return await Task.FromResult<Token>(null);
        }

        public Task<Token> GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("username", user.NormalizedUserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString("D")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
            };

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Id.ToString("N"),"Login"),
                claims
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            var tokenResult = new Token()
            {
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK"
            };

            return Task.FromResult(tokenResult);
        }

    }
}
