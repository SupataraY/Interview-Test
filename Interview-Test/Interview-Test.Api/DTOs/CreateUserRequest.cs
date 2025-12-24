namespace Interview_Test.Api.DTOs;

public class CreateUserRequest
{
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public UserProfileDto UserProfile { get; set; } = new();
    public List<int> RoleIds { get; set; } = new();
}

public class UserProfileDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
}
