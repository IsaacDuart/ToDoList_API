using System.Configuration;
using System.Runtime.Intrinsics.X86;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ScottBrady91.AspNetCore.Identity;
using ToDoList.API.ModelViews.CreateModelViews;
using ToDoList.API.Token;
using ToDoList.API.ViewModels;
using ToDoList.API.ViewModels.UpdateViewModels;
using ToDoList.Application.Config;
using ToDoList.Application.DTO;
using ToDoList.Application.DTO.CreateDTOs;
using ToDoList.Application.DTO.EntitiesDTOs;
using ToDoList.Application.DTO.UpdateDTOs;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Services;
using ToDoList.Domain.Entities;
using ToDoList.Infra;
using ToDoList.Infra.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region JWT

var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KeyConfig.Secret));

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = secretKey,
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

#endregion

#region DI

#region DependecyInjection
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IPasswordHasher<User>, Argon2PasswordHasher<User>>();
builder.Services.AddScoped<IAuthService, AuthService>();
#endregion

#region AutoMapper
AutoMapperConfiguration();
void AutoMapperConfiguration()
{
    var autoMapperConfig = new MapperConfiguration(cfg =>
    {
        #region User
        cfg.CreateMap<UserDTO, User>().ReverseMap();
        cfg.CreateMap<UserCreateDTO, User>().ReverseMap();
        cfg.CreateMap<UserUpdateDTO, User>().ReverseMap();

        cfg.CreateMap<UserDTO, CreateUserViewModel>().ReverseMap();
        cfg.CreateMap<UserDTO, UpdateUserViewModel>().ReverseMap();

        cfg.CreateMap<UserCreateDTO, CreateUserViewModel>().ReverseMap();
        cfg.CreateMap<UserUpdateDTO, UpdateUserViewModel>().ReverseMap();
        #endregion
        
        #region Assignment
        cfg.CreateMap<AssignmentDTO, Assignment>().ReverseMap();
        cfg.CreateMap<AssignmentCreateDTO, Assignment>().ReverseMap();
        cfg.CreateMap<AssignmentUpdateDTO, Assignment>().ReverseMap();
                
        cfg.CreateMap<AssignmentCreateDTO, AssignmentDTO>().ReverseMap();
        cfg.CreateMap<AssignmentUpdateDTO, AssignmentDTO>().ReverseMap();
                
        cfg.CreateMap<AssignmentCreateDTO, CreateAssignmentViewModel>().ReverseMap();
        cfg.CreateMap<AssignmentUpdateDTO, UpdateAssignmentViewModel>().ReverseMap();
        #endregion

        #region AssignmentList
        cfg.CreateMap<AssignmentListDTO, AssignmentList>().ReverseMap();
        cfg.CreateMap<AssignmentListCreateDTO, AssignmentList>().ReverseMap();
        cfg.CreateMap<AssignmentListUpdateDTO, AssignmentList>().ReverseMap();

        cfg.CreateMap<AssignmentListCreateDTO, AssignmentListDTO>().ReverseMap();
        cfg.CreateMap<AssignmentListUpdateDTO, AssignmentListDTO>().ReverseMap();
                
        cfg.CreateMap<AssignmentListCreateDTO, CreateAssignmentListViewModel>().ReverseMap();
        cfg.CreateMap<AssignmentListUpdateDTO, UpdateAssignmentListViewModel>().ReverseMap();
        #endregion

        cfg.CreateMap<LoginDTO, LoginViewModel>().ReverseMap();
        
    });
    builder.Services.AddSingleton(autoMapperConfig.CreateMapper());
    
    
}
#endregion

builder.Services.AddCors(x => x.AddPolicy("default", x => x
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()));

builder.Services.AddSingleton(d => builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(builder
    .Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder
    .Configuration.GetConnectionString("DefaultConnection"))));

#endregion


#region Swagger

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "To-Do List",
        Version = "v1",
        Description = "API Construída para gerenciar tarefas."
    });

    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insert Token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }  // Escopos, caso necessário
        }
    });

});

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("default");
app.UseAuthentication(); //JWT token
app.UseAuthorization();

app.MapControllers();

app.Run();