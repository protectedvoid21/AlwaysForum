﻿using System.Security.Claims;

namespace AlwaysForum.Extensions; 

public static class ClaimsPrincipalExtensions {
    public static string GetById(this ClaimsPrincipal claimsPrincipal) {
        return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}