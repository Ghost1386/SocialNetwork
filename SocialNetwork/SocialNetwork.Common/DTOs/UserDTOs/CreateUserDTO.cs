namespace SocialNetwork.Common.DTOs.UserDTOs;

public class CreateUserDTO
{
    public string Email { get; set; }

    public string Telephone { get; set; }
    
    public string Password { get; set; }
    
    public int Age { get; set; }
    
    public string UserStatus { get; set; }
    
    public string UserLocated { get; set; }
    
    public string UserEducation { get; set; }
    
    public byte[] UserPhoto { get; set; }
    
    public int MaritalStatus { get; set; }
    
    public string Job { get; set; }
}