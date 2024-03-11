using WebApplication1.DTO;
using WebApplication1.Enums;

namespace WebApplication1.Entity
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public PermissionType? PermissionType { get; set; }
        public bool? Status { get; set; } = true;

        public User()
        {
                
        }

        public User(AddUserDTO addUserDTO)
        {
            Name = addUserDTO.Name;
            UserName = addUserDTO.UserName;
            Password = addUserDTO.Password;
            PermissionType = addUserDTO.PermissionType;
            Status = addUserDTO.Status;
        }

        public User(EditUserDTO editUserDTO)
        {
            Name = editUserDTO.Name;
        }
    }
}
