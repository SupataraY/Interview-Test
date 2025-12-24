using Interview_Test.Models;

namespace Interview_Test.Repositories;

public static class Data
{
    public static List<UserModel> Users =>
    [
        new UserModel
        {
            Id = Guid.Parse("F90810B6-E017-431A-9DAE-A4BA7F9BC865"),
            UserId = "user02",
            Username = "Bob.M.Jackson",
            UserProfile = new UserProfileModel
            {
                FirstName = "Bob",
                LastName = "Jackson",
                Age = 28
            },
            UserRoleMappings = new List<UserRoleMappingModel>
            {
                new UserRoleMappingModel
                {
                    UserId = Guid.Parse("F90810B6-E017-431A-9DAE-A4BA7F9BC865"),
                    RoleId = 3 // document operation
                }
            }
        },
        new UserModel
        {
            Id = Guid.Parse("02CE43A4-A378-4B30-B52E-227EFA6B696E"),
            UserId = "user01",
            Username = "John.D.Smith",
            UserProfile = new UserProfileModel
            {
                FirstName = "John",
                LastName = "Smith",
                Age = null
            },
            UserRoleMappings = new List<UserRoleMappingModel>
            {
                new UserRoleMappingModel
                {
                    UserId = Guid.Parse("02CE43A4-A378-4B30-B52E-227EFA6B696E"),
                    RoleId = 1 // pick operation
                },
                new UserRoleMappingModel
                {
                    UserId = Guid.Parse("02CE43A4-A378-4B30-B52E-227EFA6B696E"),
                    RoleId = 2 // pack operation
                },
                new UserRoleMappingModel
                {
                    UserId = Guid.Parse("02CE43A4-A378-4B30-B52E-227EFA6B696E"),
                    RoleId = 3 // document operation
                }
            }
        }
    ];
}