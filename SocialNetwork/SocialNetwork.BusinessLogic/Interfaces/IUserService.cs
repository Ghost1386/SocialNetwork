using SocialNetwork.Common.DTOs.UserDTOs;
using SocialNetwork.Models.Model;

namespace SocialNetwork.BusinessLogic.Interfaces;

public interface IUserService
{
    IEnumerable<User> Get();

    User Get(int id);

    User Get(string email, string password);

    void Create(CreateUserDTO model);

    void Edit(int id, EditUserDTO model);

    void Delete(DeleteUserDTO model);
}