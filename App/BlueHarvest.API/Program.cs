using BlueHarvest.API.Controllers;
using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Models;
using BlueHarvest.Shared.DTOs.Cosmic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assemblies = new[]
{
   Assembly.GetAssembly(typeof(IMongoDocument)),
   Assembly.GetAssembly(typeof(BaseController)),
   Assembly.GetAssembly(typeof(StarClusterDto)),
};

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
   .AddEndpointsApiExplorer()
   .AddSwaggerGen()
   // .AddFluentValidation(options =>
   // {
   //    options.RegisterValidatorsFromAssemblies(assemblies);
   //    //options.DisableDataAnnotationsValidation = false,
   //    //options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
   // })
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
   .AddBlueHarvestCommon(assemblies);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
   app.UseExceptionHandler("/error-dev");
}
else
{
   app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
