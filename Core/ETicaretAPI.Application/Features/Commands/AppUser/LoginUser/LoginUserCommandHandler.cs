using ETicaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using P = ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<P.AppUser> _userManager; 
        readonly SignInManager<P.AppUser> _signInManager;

        public LoginUserCommandHandler(SignInManager<P.AppUser> signInManager, UserManager<P.AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            //username gelirse user'a atar, eğer username yerine email yazmışsa null gelecek ve if bloğunun içerisinde user'a emaili atanacak
            P.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user == null)
                throw new NotFoundUserException("Kullanıcı adı veya şifre hatalı.");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded) //Authentaication başarılı
            {
                //....Yetkileri belirlememiz gerekiyor
            }

            return new();   
        }
    }
}
