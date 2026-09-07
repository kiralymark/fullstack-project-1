using fullstack_project_1.Components;
using fullstack_project_1.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Identity
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>();
builder.Services.AddIdentity<AspNetUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

//  Add JWT Bearer
}).AddJwtBearer(options =>
{
    // Retrieve configuration values using builder.Configuration before setting options
    string secretString = builder.Configuration.GetSection("JWT")["Secret"]
            ?? throw new InvalidOperationException("Configuration string 'JWT:Secret' not found.");
    string issuerString = builder.Configuration.GetSection("JWT")["Issuer"]
            ?? throw new InvalidOperationException("Configuration string 'JWT:Issuer' not found.");
    string audienceString = builder.Configuration.GetSection("JWT")["Audience"]
            ?? throw new InvalidOperationException("Configuration string 'JWT:Audience' not found.");

    options.SaveToken = true;
    options.RequireHttpsMetadata = false;       // to authenticate users using 'HTTP' requests
    options.TokenValidationParameters = new TokenValidationParameters()     // to define a way in which to validate the token that comes from the clients
    {
        ValidateIssuerSigningKey = true,
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:Secret"])),  
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretString)), // set issuer key source.

        ValidateIssuer = true,
        //ValidIssuer = Configuration["JWT:Issuer"],
        ValidIssuer = issuerString,

        ValidateAudience = true,
        //ValidAudience = Configuration["JWT:Audience"],
        ValidAudience = audienceString,

    }; 
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found");
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
