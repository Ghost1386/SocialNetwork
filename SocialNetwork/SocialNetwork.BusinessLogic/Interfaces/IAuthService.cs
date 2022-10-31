using SocialNetwork.Common.DTOs.AuthDTOs;

namespace SocialNetwork.BusinessLogic.Interfaces;

public interface IAuthService
{
    string Login(AuthUserDTO model);

    void Register(RegisterUserDTO model);
}