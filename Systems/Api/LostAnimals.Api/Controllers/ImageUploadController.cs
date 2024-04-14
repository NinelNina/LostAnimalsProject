using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace LostAnimals.Api.Controllers;


[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "LostAnimals.API")]
[Route("v{version:apiVersion}/[controller]")]

public class ImageUploadController : ControllerBase
{
    public IWebHostEnvironment environment;

    public ImageUploadController(IWebHostEnvironment environment)
    {
        this.environment = environment;
    }

    public class FileUploadAPI
    {
        public IFormFile files { get; set; }
    }

    [HttpPost]
    public async Task<string> SaveFile(FileUploadAPI objFile)
    {
        try
        {
            if (objFile.files.Length > 0)
            {
                if (!Directory.Exists(environment.WebRootPath + "\\Upload\\"))
                {
                    Directory.CreateDirectory(environment.WebRootPath + "\\Upload\\");
                }
                using (FileStream fileStream = System.IO.File.Create(environment.WebRootPath + "\\Upload\\" + objFile.files.FileName))
                {
                    objFile.files.CopyTo(fileStream);
                    fileStream.Flush();
                    return "\\Upload\\" + objFile.files.FileName;
                }
            }
            else
            {
                return "Failed";
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }
}
