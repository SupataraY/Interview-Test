using Interview_Test.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Interview_Test.Infrastructure;

public class InterviewTestDbContext : DbContext
{
    public InterviewTestDbContext(DbContextOptions<InterviewTestDbContext> options) : base(options)
    {
    }
    
    // DbSet สำหรับทุก Entity
    public DbSet<UserModel> UserTb { get; set; }
    public DbSet<UserProfileModel> UserProfileTb { get; set; }
    public DbSet<RoleModel> RoleTb { get; set; }
    public DbSet<PermissionModel> PermissionTb { get; set; }
    public DbSet<UserRoleMappingModel> UserRoleMappingTb { get; set; }
    public DbSet<RolePermissionModel> RolePermissionTb { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // ========== User-Role Many-to-Many Configuration ==========
        modelBuilder.Entity<UserRoleMappingModel>(entity =>
        {
            // Composite Key: UserId + RoleId
            entity.HasKey(urm => new { urm.UserId, urm.RoleId });
            
            // Relationship: User -> UserRoleMapping
            entity.HasOne(urm => urm.User)
                  .WithMany(u => u.UserRoleMappings)
                  .HasForeignKey(urm => urm.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            // Relationship: Role -> UserRoleMapping
            entity.HasOne(urm => urm.Role)
                  .WithMany(r => r.UserRoleMappings)
                  .HasForeignKey(urm => urm.RoleId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        
        // ========== Role-Permission Many-to-Many Configuration ==========
        modelBuilder.Entity<RolePermissionModel>(entity =>
        {
            // Composite Key: RoleId + PermissionId
            entity.HasKey(rp => new { rp.RoleId, rp.PermissionId });
            
            // Relationship: Role -> RolePermission
            entity.HasOne(rp => rp.Role)
                  .WithMany(r => r.RolePermissions)
                  .HasForeignKey(rp => rp.RoleId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            // Relationship: Permission -> RolePermission
            entity.HasOne(rp => rp.Permission)
                  .WithMany(p => p.RolePermissions)
                  .HasForeignKey(rp => rp.PermissionId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

public class InterviewTestDbContextDesignFactory : IDesignTimeDbContextFactory<InterviewTestDbContext>
{
    public InterviewTestDbContext CreateDbContext(string[] args)
    {
        // อ่าน configuration จาก appsettings.Development.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Interview-Test.Api"))
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        var optionsBuilder = new DbContextOptionsBuilder<InterviewTestDbContext>()
            .UseSqlServer(connectionString, opts => opts.CommandTimeout(600));

        return new InterviewTestDbContext(optionsBuilder.Options);
    }
}