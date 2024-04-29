namespace LostAnimals.Web.Pages.Auth.Services;

public class Result
{
    public bool Successful { get; set; }
    public IEnumerable<string> Errors { get; set; }

    public Result(bool successful, IEnumerable<string> errors = null)
    {
        Successful = successful;
        Errors = errors ?? new string[0];
    }
}