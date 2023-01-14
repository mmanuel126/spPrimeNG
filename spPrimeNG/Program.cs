using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using sportprofiles.DBContextModels;
using sportprofiles.Repository;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();

ConfigurationManager configuration = builder.Configuration;

//authentication
var sharedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryVerySecretKey"));

builder.Services.AddAuthentication(
options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters.ValidateLifetime = true;
    options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(5);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Specify the key used to sign the token:
        IssuerSigningKey = sharedKey,
        RequireSignedTokens = true,
        // Other options...
        ValidAudience = configuration["Jwt:Issuer"],
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

//authorization
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("SurveyCreator", p =>
    {
        // Using value text for demo show, else use enum : ClaimTypes.Role
        p.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "SurveyCreator");
    });
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sports Profile API",
        Description = "RESTful API web service for the Sports Profile (SP) social networking application." +
        "<br/><br/>Author: Marc Manuel<br/><br/>To experiment with the API functionalities, please send email to <b>marc_manuel@hotmail.com</b> to obtain test account and instructions.",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}
                     }
                 });

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "SPapi.xml");
    c.IncludeXmlComments(filePath);
    c.EnableAnnotations();
});

builder.Services.AddDbContext<dbContexts>(op => op.UseSqlServer(configuration["ConnectionStrings:ESdbConnString"],
                options => options.EnableRetryOnFailure()));

builder.Services.AddTransient<ICommonRepository, CommonRepository>();
builder.Services.AddTransient<ILoggingRepository, LoggingRepository>();
builder.Services.AddTransient<IMemberRepository, MemberRepository>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<IConnectionRepository, ConnectionRepository>();
// services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<INetworkRepository, NetworkRepository>();
// services.AddTransient<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddTransient<ISettingRepository, SettingRepository>();

//use to add logging of errors for angular client app -- see commoncontroller logs http api method  
builder.Services.AddLogging(loggingBuilder =>
{
    var loggingSection = configuration.GetSection("Logging");
    loggingBuilder.AddFile(loggingSection);
});

//Create the log4net provider
builder.Logging.AddLog4Net();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sports Profile API V1");
});

app.UseRouting();

app.UseCors(option => option
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();

