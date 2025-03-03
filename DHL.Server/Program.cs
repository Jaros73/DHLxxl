using DHL.Server.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
var builder = WebApplication.CreateBuilder(args);
var authScheme = builder.Environment.IsDevelopment() ? "TestAuth" : JwtBearerDefaults.AuthenticationScheme;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(authScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://enterprise-oauth.company.com"; // OAuth 2.0 server
        options.Audience = "your-api";
        options.RequireHttpsMetadata = false;
    })
    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestAuth", null); // Testovací autentizace pro vývoj

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
