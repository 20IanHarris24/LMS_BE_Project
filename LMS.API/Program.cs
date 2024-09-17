
using Companies.API.Extensions;
using LMS.API.Data;
using LMS.API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseContext") ?? throw new InvalidOperationException("Connection string 'DatabaseContext' not found.")));

        // Add services to the container.
        builder.Services.AddControllers(configure =>
        {
            //configure.ReturnHttpNotAcceptable = true;
            //var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole("Teacher").Build();
            //configure.Filters.Add(new AuthorizeFilter(policy));
        });
        builder.Services.ConfigureJwt(builder.Configuration);
        builder.Services.ConfigureCors();
        builder.Services.ConfigureServices();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddIdentityCore<ApplicationUser>(opt =>
        {
            opt.Password.RequireDigit = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 3;

        })
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<DatabaseContext>()
               .AddDefaultTokenProviders();



        //ToDo: AddIdentityCore 
        //ToDo: AddDbContext

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        //ToDo: AddAuthentication
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
