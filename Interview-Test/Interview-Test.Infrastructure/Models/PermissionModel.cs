using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interview_Test.Models;

[Table("PermissionTb")]
public class PermissionModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long PermissionId { get; set; }
    [Required]
    [Column(TypeName = "varchar(100)")]
    public string Permission { get; set; }
    
    // Navigation Property สำหรับ Many-to-Many กับ Role
    public ICollection<RolePermissionModel>? RolePermissions { get; set; }
}