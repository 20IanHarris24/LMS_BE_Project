
using Companies.API.Extensions;
using LMS.API.Data;
using LMS.API.Models.Dtos.Mapper;
using LMS.API.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LMS.API;

public class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseContext") ?? throw new InvalidOperationException("Connection string 'DatabaseContext' not found.")));

        //Mapper
        builder.Services.AddAutoMapper(typeof(MapperManager));


        // Add services to the container.
        builder.Services.AddControllers(configure =>
        {
            //configure.ReturnHttpNotAcceptable = true;
            //var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole("Teacher").Build();
            //configure.Filters.Add(new AuthorizeFilter(policy));
        }).AddNewtonsoftJson();
        builder.Services.ConfigureCors();
        builder.Services.ConfigureServices();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.ConfigureJwt(builder.Configuration);
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'"
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
        });

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


        app.UseAuthentication();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
