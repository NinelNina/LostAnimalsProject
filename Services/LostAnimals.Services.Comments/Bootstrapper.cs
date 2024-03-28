using Microsoft.Extensions.DependencyInjection;

namespace LostAnimals.Services.Comments;

public static class Bootstrapper
{
    public static IServiceCollection AddCommentService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICommentService, CommentService>();
    }
}
