using Asp.Versioning;
using AutoMapper;
using LostAnimals.ViewModels.Account;
using LostAnimals.Common.Security;
using LostAnimals.Common.Validator;
using LostAnimals.Services.EmailSender;
using LostAnimals.Services.Logger;
using LostAnimals.Services.UserAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    private readonly IModelValidator<RegisterUserAccountViewModel> registerUserAccountModelValidator;

    public AccountsController(IMapper mapper, IAppLogger logger, IUserAccountService userAccountService, IEmailSenderService emailSenderService,
        IModelValidator<RegisterUserAccountViewModel> registerUserAccountModelValidator)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.userAccountService = userAccountService;
        this.emailSenderService = emailSenderService;
        this.registerUserAccountModelValidator = registerUserAccountModelValidator;
    }

    [HttpPost("")]
    public async Task<UserAccountViewModel> Register([FromQuery] RegisterUserAccountViewModel request)
    {
        registerUserAccountModelValidator.Check(request);

        var requestModel = mapper.Map<RegisterUserAccountModel>(request);

        var result = await userAccountService.Create(requestModel);

        await SendConfirmationLink(result.Id);

        var user = mapper.Map<UserAccountViewModel>(result);

        return user;
    }

    [HttpGet("")]
    [Authorize(AppScopes.UsersRead)]
    public async Task<IEnumerable<UserAccountViewModel>> GetAll()
    {
        var users = await userAccountService.GetAll();

        IEnumerable<UserAccountViewModel> result = users.Select(mapper.Map<UserAccountViewModel>);

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var user = await userAccountService.GetById(id);

        if (user == null)
            return NotFound();

        var result = mapper.Map<UserAccountViewModel>(user);

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
            var subject = "Подтверждение электронной почты на сайте Lost Animals | Email confirmation on Lost Animals website";
            var content = $" <p>Для подтверждения Вашего аккаунта на сайте Lost Animals перейдите по ссылке ниже. Ссылка будет доступна в течение одного дня:</p>\r\n" +
                $"    <p><a href=\"{confirmationLink}\">Подтвердить email</a></p>\r\n" +
                $"    <p>Если вы не регистрировались на сайте Lost Animals, просто проигнорируйте это письмо.</p>\r\n" +
                $"    <p>С уважением,<br>команда Lost Animals.</p> \r\n" +
                $"    <hr>\r\n\r\n" +
                $"    <p>To confirm your account on the Lost Animals website, follow the link below. The link will be available for one day:</p>\r\n" +
                $"    <p><a href=\"{confirmationLink}\">Confirm email</a></p>\r\n" +
                $"    <p>If you did not register on the Lost Animals website, simply ignore this email.</p>\r\n" +
                $"    <p>Sincerely,<br>the Lost Animals team.</p>";

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