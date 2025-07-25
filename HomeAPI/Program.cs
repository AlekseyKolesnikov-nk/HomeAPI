using FluentValidation;
using FluentValidation.AspNetCore;
using HomeAPI.Configuration;
using HomeAPI.Contracts.Validation;
using HomeAPI.Data;
using HomeAPI.Data.Repos;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HomeAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var assembly = Assembly.GetAssembly(typeof(MappingProfile));
        builder.Services.AddAutoMapper(assembly);

        string connection = builder.Configuration.GetSection("ConnectionStrings").GetValue(typeof(string), "DefaultConnection").ToString();
        builder.Services.AddDbContext<HomeAPIContext>(options => options.UseSqlServer(connection));

        builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
        builder.Services.AddScoped<IRoomRepository, RoomRepository>();

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<AddDeviceRequestValidator>();

        builder.Services.Configure<HomeOptions>(new ConfigurationBuilder().AddJsonFile("HomeOptions.json").Build());

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}