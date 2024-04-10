namespace LostAnimals.Services.EmailSender;

public interface IEmailSenderService
{
    Task SendEmail(Message message);
}
