using System.Reflection;
using BlueHarvest.API.Controllers;
using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Models;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var logger = Log.Logger = new LoggerConfiguration()
   .ReadFrom.Configuration(builder.Configuration)
   .CreateLogger();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
Log.Verbose($"BlueHarvest API starting up on environment: '{environment}'.");
Log.Debug($"BlueHarvest API starting up on environment: '{environment}'.");
Log.Information($"BlueHarvest API starting up on environment: '{environment}'.");

// Add services to the container.
builder.Host.UseSerilog();
builder.Services.AddControllers();

var assemblies = new[] {Assembly.GetAssembly(typeof(IRootModel)), Assembly.GetAssembly(typeof(BaseController)),};

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
   .AddEndpointsApiExplorer()
   .AddSwaggerGen()
   .AddFluentValidation(options =>
   {
      options.RegisterValidatorsFromAssemblies(assemblies);
      //options.DisableDataAnnotationsValidation = false,
      //options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
   })
   .AddApiVersioning(options =>
   {
      options.ReportApiVersions = true;
      options.UseApiBehavior = false;
   })
   .AddVersionedApiExplorer(options =>
   {
      options.GroupNameFormat = "'v'VVV";
      options.SubstituteApiVersionInUrl = true;
   })
   .AddBlueHarvestMongo(builder.Configuration)
   .AddBlueHarvestCommon(assemblies)
   ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
