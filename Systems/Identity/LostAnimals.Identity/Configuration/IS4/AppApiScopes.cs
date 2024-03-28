using Duende.IdentityServer.Models;
using LostAnimals.Common.Security;

namespace LostAnimals.Identity.Configuration.IS4;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
    new List<ApiScope>
    {
            new ApiScope(AppScopes.AnimalKindsRead, "Read"),
            new ApiScope(AppScopes.AnimalKindsWrite, "Write")
        };
}