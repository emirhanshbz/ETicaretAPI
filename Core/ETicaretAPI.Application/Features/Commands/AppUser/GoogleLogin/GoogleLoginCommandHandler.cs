using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using P = ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly IConfiguration _configuration;
        readonly UserManager<P.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;

        public GoogleLoginCommandHandler(UserManager<P.AppUser> userManager, IConfiguration configuration, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var clientId = _configuration["Authentication:Google"];

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            { 
                Audience = new List<string> { clientId }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

            var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
            P.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if(user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        NameSurname = payload.Name,
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
                
            }
            if (result)
                await _userManager.AddLoginAsync(user, info);  //AspNetUserLogins
            else
                throw new Exception("Invalid external authentication");

            Token token = _tokenHandler.CreateAccessToken(5);

            return new()
            {
                Token = token
            };
        }
    }
}
