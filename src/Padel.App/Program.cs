
using Padel.App;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAppLogger(builder.Configuration);

builder.Services.AddDataBase(
    builder.Configuration,
    builder.Environment.IsDevelopment());

builder.Services.AddFeatures();
builder.Services.AddTools();

builder.Services.AddSecurity(builder.Configuration);

var app = builder.Build();

var enableAuth = builder.Configuration.GetValue<bool>("Security:EnableAuthentication");

if (enableAuth)
{
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapMcp().RequireAuthorization();
}
else
{
    app.MapMcp();
}

app.Run();