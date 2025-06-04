using LostAnimals.Web.Pages.PhotoSearch.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace LostAnimals.Web.Pages.PhotoSearch.Services;

public interface IPhotoSearchService
{
    Task<List<SearchResult>> SearchByPhotoAsync(IBrowserFile file);
}
