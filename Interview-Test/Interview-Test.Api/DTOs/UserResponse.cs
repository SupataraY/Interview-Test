namespace Interview_Test.Api.DTOs;

public class UserResponse
{
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public List<RoleDto> Roles { get; set; } = new();
    public List<PermissionDto> Permissions { get; set; } = new();
}

public class RoleDto
{
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
}

public class PermissionDto
{
    public int PermissionId { get; set; }
    public string Permission { get; set; } = string.Empty;
}
