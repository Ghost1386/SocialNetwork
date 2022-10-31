using AutoMapper;
using SocialNetwork.BusinessLogic.Interfaces;
using SocialNetwork.Common.DTOs.UserDTOs;
using SocialNetwork.Models;
using SocialNetwork.Models.Model;

namespace SocialNetwork.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public UserService(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<User> Get()
    {
        return _context.Users;
    }

    public User Get(int id)
    {
        return _context.Users.Where(x => x.Id == id).FirstOrDefault();
    }

    public User Get(string email, string password)
    {
        return _context.Users.Where(x => x.Email == email && 
                                         x.Password == password).FirstOrDefault();
    }

    public void Create(CreateUserDTO model)
    {
        var user = _mapper.Map<CreateUserDTO, User>(model);

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Edit(int id, EditUserDTO model)
    {
        var user = Get(id);

        if (user.Id == 0)
        {
            return;
        }
        
        var editUser = _mapper.Map<EditUserDTO, User>(model);

        _context.Users.Update(editUser);
        _context.SaveChanges();
    }

    public void Delete(DeleteUserDTO model)
    {
        _context.Users.Remove(Get(model.Email, model.Password));
        _context.SaveChanges();
    }
}