using Data;
using Repositories.CommentRepository;
using Repositories.PostRepository;
using Repositories.UserRepository;
using Repositories.OriginalPostRepository; // Add the OriginalPostRepository namespace
using Repositories;
using Services.PostServices;
using Services.OriginalPostServices; // Add the OriginalPostService namespace
using Services.CommentServices;
using Services.UserServices;
using Services.TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure MongoDB settings from appsettings.json
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(nameof(MongoDBSettings)));

// Add MongoDBContext as a singleton
builder.Services.AddSingleton<MongoDBContext>();

// Add repository services
builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IOriginalPostRepository, OriginalPostRepository>(); // Register OriginalPostRepository

//builder.Services.AddScoped<IPostServices, PostServices>();
builder.Services.AddScoped<IOriginalPostService, OriginalPostService>(); // Register OriginalPostService
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentServices, CommentServices>();
builder.Services.AddScoped<IUserServices, UserServices>();  // Add User Services
builder.Services.AddScoped<TokenService>();  // Add Token Service

// CORS Policy Definition
var allowedOrigins = "AllowFrontend";  // Give the CORS policy a name
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")  // Allow this specific origin
                  .AllowAnyMethod()                     // Allow all HTTP methods (GET, POST, PUT, etc.)
                  .AllowAnyHeader()                     // Allow all headers
                  .AllowCredentials();                  // Allow cookies or authentication credentials
        });
});

// JWT Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Add controllers
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS before authentication and authorization middleware
app.UseCors(allowedOrigins);

app.UseAuthentication();  // Add Authentication middleware
app.UseAuthorization();

app.MapControllers();

app.Run();
