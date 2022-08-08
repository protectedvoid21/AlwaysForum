using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data; 

public static class GlobalConstants {
    public const string AdminUserName = "Admin";
    public const string AdminPassword = "Admin123";
    public const string AdminRoleName = "Admin";
    public const string AdminEmail = "admin@AlwaysForum.com";

    public static readonly string[] RequiredRoles = { "Moderator", "User" };

    public const int MaximumPostDescriptionLength = 50;
}