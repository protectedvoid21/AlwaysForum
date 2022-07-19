using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data; 

public static class GlobalConstants {
    public static string AdminUserName = "Admin";
    public static string AdminPassword = "Admin123";

    public static string[] RequiredRoles = { "Admin", "Moderator", "User" };
}