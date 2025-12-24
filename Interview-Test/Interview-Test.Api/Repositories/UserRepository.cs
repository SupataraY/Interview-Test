using Interview_Test.Infrastructure;
using Interview_Test.Models;
using Interview_Test.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Interview_Test.Repositories;

public class UserRepository : IUserRepository
{
    private readonly InterviewTestDbContext _context;

    public UserRepository(InterviewTestDbContext context)
    {
        _context = context;
    }

    public dynamic GetUserById(string id)
    {
        // Query User พร้อม Join กับ UserRoleMapping, Role, RolePermission, Permission
        var user = _context.UserTb
            .Include(u => u.UserProfile)
            .Include(u => u.UserRoleMappings)
                .ThenInclude(urm => urm.Role)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
            .Where(u => u.UserId == id)
            .Select(u => new
            {
                id = u.Id,
                userId = u.UserId,
                username = u.Username,
                firstName = u.UserProfile.FirstName,
                lastName = u.UserProfile.LastName,
                age = u.UserProfile.Age,
                roles = u.UserRoleMappings
                    .Select(urm => new
                    {
                        roleId = urm.Role.RoleId,
                        roleName = urm.Role.RoleName
                    })
                    .Distinct()
                    .ToList(),
                permissions = u.UserRoleMappings
                    .SelectMany(urm => urm.Role.RolePermissions)
                    .Select(rp => rp.Permission.Permission)
                    .Distinct()
                    .OrderBy(p => p)
                    .ToList()
            })
            .FirstOrDefault();

        return user;
    }

    public int CreateUser(UserModel user)
    {
        // Validate input
        if (user == null)
            throw new ArgumentNullException(nameof(user));
        
        if (user.UserProfile == null)
            throw new ArgumentNullException(nameof(user.UserProfile));

        // 1. Map ข้อมูลจาก Data.cs → Entity
        var newUser = new UserModel
        {
            Id = user.Id,
            UserId = user.UserId,
            Username = user.Username,
            UserProfile = new UserProfileModel
            {
                FirstName = user.UserProfile.FirstName,
                LastName = user.UserProfile.LastName,
                Age = user.UserProfile.Age
            },
            UserRoleMappings = new List<UserRoleMappingModel>()
        };

        // เพิ่ม Role Mappings (ความสัมพันธ์ User-Role)
        if (user.UserRoleMappings != null && user.UserRoleMappings.Any())
        {
            foreach (var roleMapping in user.UserRoleMappings)
            {
                if (roleMapping?.Role != null)
                {
                    newUser.UserRoleMappings.Add(new UserRoleMappingModel
                    {
                        UserId = user.Id,
                        RoleId = roleMapping.Role.RoleId
                    });
                }
            }
        }

        // 2. เพิ่ม User ลงใน DbContext
        _context.UserTb.Add(newUser);

        // 3. บันทึกการเปลี่ยนแปลงและ return จำนวน row ที่ถูก affect
        return _context.SaveChanges();
    }
}