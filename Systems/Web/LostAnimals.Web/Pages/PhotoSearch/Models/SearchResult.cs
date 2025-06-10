namespace LostAnimals.Web.Pages.PhotoSearch.Models;

public class SearchResult
{
    public string Id { get; set; }
    public float Score { get; set; }
    public string AnimalType { get; set; }
    public string ImagePath { get; set; }
    public string NoteId { get; set; }
    public string PhotoId { get; set; }
    public Dictionary<string, object> Metadata { get; set; }
}
