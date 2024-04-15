using Asp.Versioning;
using AutoMapper;
using LostAnimals.Services.EmailSender;
using LostAnimals.Services.UserAccount;
using LostAnimals.Services.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LostAnimals.Common.Security;

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

        await SendConfirmationLink(user.Id);

        return user;
    }

    [HttpGet("")]
    [Authorize(AppScopes.UsersRead)]
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
    
    [HttpPost("SendConfirmationLink/{id:Guid}")]
    public async Task<IActionResult> SendConfirmationLink([FromRoute] Guid id)
    {
        var user = await userAccountService.GetById(id);

        if (user == null)
            return NotFound();

        var token = await userAccountService.GenerateEmailConfirmationToken(id);

        if (token != null)
        {
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Accounts", new { token, email = user.Email }, Request.Scheme);

            var to = user.Email;
            var subject = "Lost Animals sent user confirmation link";
            var content = $"To confirm your account on the Lost Animals website, follow the link below. The link will be available for one day:\n{confirmationLink}";

            var message = new Message(new string[] { to }, subject, content);
            try
            {
                await emailSenderService.SendEmail(message);
            }
            catch (Exception e)
            {
                logger.Error("Error. Message not sent. ", e);

                return BadRequest();
            }

            return Ok(message);
        }

        return BadRequest();
    }

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var result = await userAccountService.ConfirmEmailAsync(email, token);
        if (result.Succeeded)
        {
            logger.Information($"User email {email} is confirmed.");

            return Ok("User email is confirmed.");
        }
        else
        {
            logger.Error($"User email {email} is not confirmed.");
            logger.Error($"Errors: {string.Join(", ", result.Errors)}");

            return BadRequest();
        }
    }
}