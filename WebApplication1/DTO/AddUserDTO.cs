using WebApplication1.Enums;

namespace WebApplication1.DTO
{
    public class AddUserDTO
    {
        public string? Name { get; set; }
        public string? UserName { get;  set; }
        public string? Password { get;  set; }
        public PermissionType? PermissionType { get;  set; }
        public bool? Status { get;  set; }
    }
}
