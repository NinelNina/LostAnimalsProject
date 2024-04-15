using Duende.IdentityServer.Models;
using LostAnimals.Common.Security;

namespace LostAnimals.Identity.Configuration.IS4;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
    new List<ApiScope>
    {
            //new ApiScope(AppScopes.AnimalKindsRead, "ReadAnimalKinds"),
            new ApiScope(AppScopes.AnimalKindsWrite, "AnimalKindsWrite"),
            new ApiScope(AppScopes.BreedsWrite, "BreedsWrite"),
            new ApiScope(AppScopes.NotesWrite, "NotesWrite"),
            new ApiScope(AppScopes.PhotosWrite, "PhotosWrite"),
            new ApiScope(AppScopes.NoteCategoriesWrite, "NoteCategoriesWrite"),
            new ApiScope(AppScopes.CommentsWrite, "CommentsWrite"),
            new ApiScope(AppScopes.UsersRead, "UsersRead"),
};
}