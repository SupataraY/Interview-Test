using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interview_Test.Models;

[Table("RolePermissionTb")]
public class RolePermissionModel
{
    // Composite Key: RoleId + PermissionId
    [Required]
    public int RoleId { get; set; }
    
    [Required]
    public long PermissionId { get; set; }
    
    // Navigation Properties
    [ForeignKey("RoleId")]
    public RoleModel? Role { get; set; }
    
    [ForeignKey("PermissionId")]
    public PermissionModel? Permission { get; set; }
}
