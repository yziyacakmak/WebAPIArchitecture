namespace CleanApp.API.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerGenExt(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        return services;
    }

    public static IApplicationBuilder UseSwaggerExt(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }


}

