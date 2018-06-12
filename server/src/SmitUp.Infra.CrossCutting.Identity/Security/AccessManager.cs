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
        public Task<IdentityResult> CreateUser(User user)
        {
            return _userManager.CreateAsync(user, user.PasswordHash);
        }

        public Task<User> GetUser(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public Task<User> GetUserByUsername(string username)
        {
            return _userManager.FindByNameAsync(username);
        }

        public Task<string> GenerateEmailConfirmationToken(User user)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public Task<IdentityResult> ConfirmEmailToken(User user, string token)
        {
            return _userManager.ConfirmEmailAsync(user, token);
        }

        public Task<SignInResult> ValidateCredentials(User user, string password)
        {
            return _signInManager.CheckPasswordSignInAsync(user, password, true);
        }



        public Task<Token> GenerateToken(User user)
        {
            return Task.Run(() =>
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("username", user.NormalizedUserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString("D")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
            };

                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Id.ToString("N"), "Login"),
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
                return tokenResult;
            });

        }

    }
}
