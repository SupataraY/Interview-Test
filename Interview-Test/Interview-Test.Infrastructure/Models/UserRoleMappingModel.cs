using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interview_Test.Models;

[Table("UserRoleMappingTb")]
public class UserRoleMappingModel
{
    // Composite Key: UserId + RoleId (จะถูกกำหนดใน Fluent API)
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public int RoleId { get; set; }
    
    // Navigation Properties
    [ForeignKey("UserId")]
    public UserModel? User { get; set; }
    
    [ForeignKey("RoleId")]
    public RoleModel? Role { get; set; }
}