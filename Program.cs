using fullstack_project_1.Components;
using fullstack_project_1.Data;
using fullstack_project_1.Data.Authentication;
using fullstack_project_1.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore;
//using System.Diagnostics;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Registers API Controllers and JSON Formatters
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();      // Add Razor Components

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddScoped<SubjectsService>();         // Register the SubjectsService

// IHttpClientFactory usage 
builder.Services.AddHttpClient("AuthAPI", client =>
{
    string apiURLString = builder.Configuration["JWT:Issuer"]
            ?? throw new InvalidOperationException("Configuration string 'JWT:Issuer' ('apiURLString') not found.");

    // backend API URL and port configure
    client.BaseAddress = new Uri(apiURLString);       // note, don't use url and port directly ; note, apiURLString and JWT:Issuer value is the same  
});


// Add Identity
builder.Services.AddDbContext<AppDbContext>();

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
    string secretString = builder.Configuration["JWT:Secret"]
            ?? throw new InvalidOperationException("Configuration string 'JWT:Secret' not found.");
    string issuerString = builder.Configuration["JWT:Issuer"]
            ?? throw new InvalidOperationException("Configuration string 'JWT:Issuer' not found.");
    string audienceString = builder.Configuration["JWT:Audience"]
            ?? throw new InvalidOperationException("Configuration string 'JWT:Audience' not found.");

    options.SaveToken = true;
    options.RequireHttpsMetadata = false;       // to authenticate users using 'HTTP' requests
    options.TokenValidationParameters = new TokenValidationParameters()     // to define a way in which to validate the token that comes from the clients
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretString)), // set issuer key source.

        ValidateIssuer = true,
        ValidIssuer = issuerString,

        ValidateAudience = true,
        ValidAudience = audienceString,

    }; 
});

//builder.Services.AddOpenApi(); // Register the OpenAPI services 

// Register the Swagger services 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Title, API",
        Description = "An ASP.NET Core Web API, Description",
        //TermsOfService = new Uri("https://example.com/terms"),
        TermsOfService = new Uri("#"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            //Url = new Uri("https://example.com/contact")
            Url = new Uri("#")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            //Url = new Uri("https://example.com/license")
            Url = new Uri("#")
        }
    });
});
*/

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //Debug.WriteLine("Environment.IsDevelopment");

    app.UseDeveloperExceptionPage();

    // Add the /openapi/{documentName}.json endpoint to the application
    //app.MapOpenApi();

    /*
    app.UseSwagger();   // Add Swagger
    app.UseSwaggerUI(options =>
    {
        //options.SwaggerEndpoint("/openapi/v1.json","OpenAPI V1");
        //options.SwaggerEndpoint("./openapi/v1.json","OpenAPI V1");
        //options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        //options.RoutePrefix = string.Empty;
        options.RoutePrefix = "/swagger";
    });
    */
    
} 
else
{
    //Debug.WriteLine("NOT Environment.IsDevelopment");

    // Configure the HTTP request pipeline.
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found");

// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

// Add Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

    //options.RoutePrefix = "/swagger";
});
// Redirect requests from a URL to the Swagger UI
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/swagger")
    {
        context.Response.Redirect("/swagger/index.html");
        return;
    }

    await next();
});

app.MapStaticAssets();

// Maps API Controller Endpoints
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
