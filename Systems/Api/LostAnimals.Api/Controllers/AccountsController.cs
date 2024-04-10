using Asp.Versioning;
using AutoMapper;
using LostAnimals.Services.EmailSender;
using LostAnimals.Services.UserAccount;
using LostAnimals.Services.Logger;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LostAnimals.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "LostAnimals.API")]
[Route("v{version:apiVersion}/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IAppLogger logger;
    private readonly IUserAccountService userAccountService;
    private readonly IEmailSenderService emailSenderService;

    public AccountsController(IMapper mapper, IAppLogger logger, IUserAccountService userAccountService, IEmailSenderService emailSenderService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.userAccountService = userAccountService;
        this.emailSenderService = emailSenderService;
    }

    [HttpPost("")]
    public async Task<UserAccountModel> Register([FromQuery] RegisterUserAccountModel request)
    {
        var user = await userAccountService.Create(request);
        return user;
    }

    [HttpGet("")]
    public async Task<IEnumerable<UserAccountModel>> GetAll()
    {
        var result = await userAccountService.GetAll();

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await userAccountService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("sendMessage")]
    public async Task<IActionResult> SendMessage()
    {
        var message = new Message(new string[] { "mailsenderapp@yandex.ru" }, "Test email", "This is the content from our email.");
        await emailSenderService.SendEmail(message);

        return Ok(message);
    }

    [HttpPost("ConfirmEmail/{id:Guid}")]
    public async Task<IActionResult> ConfirmEmail([FromRoute] Guid id)
    {
        var user = await userAccountService.GetById(id);

        if (user == null)
            return NotFound();

        var token = await userAccountService.GenerateEmailConfirmationToken(id);

        if (token != null)
        {
            //var confirmationLink = Url.Action(nameof(ConfirmEmail), "Accounts", new { token, email = user.Email }, Request.Scheme);
            var confirmationLink = $"{Request.Scheme}://{Request.Host}/Account/ConfirmEmail?token={token}&email={user.Email}";

            var message = new Message(new string[] { user.Email }, "User confirmation mail", $"To confirm your account on the Lost Animals website, follow the link below:\n{confirmationLink}");
            await emailSenderService.SendEmail(message);

            return Ok(message);
        }

        return BadRequest();
    }
}
