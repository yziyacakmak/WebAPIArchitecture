using App.Application.Extensions;
using App.Bus;
using App.Persistence.Extension;
using CleanApp.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllerWithFiltersExt()
    .AddSwaggerGenExt()
    .AddExceptionHandlerExt()
    .AddCachingExt()
    .AddBusExt(builder.Configuration);

builder.Services.AddRepository(builder.Configuration)
    .AddServices(builder.Configuration);

var app = builder.Build();

app.UseConfigurePipelineExt();
app.MapControllers();

app.Run();
