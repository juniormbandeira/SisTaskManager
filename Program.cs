var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ModuleAccess", policy => 
        policy.Requirements.Add(new ModuleAccessRequirement("")));
});

builder.Services.AddSingleton<IAuthorizationHandler, ModuleAccessHandler>();

var app = builder.Build();
